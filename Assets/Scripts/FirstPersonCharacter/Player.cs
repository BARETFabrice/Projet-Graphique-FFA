using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]

public class Player : NetworkBehaviour
{
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_StickToGroundForce;
    [SerializeField] private float m_GravityMultiplier;
    [SerializeField] private MouseLook m_MouseLook;
    [SerializeField] private bool m_UseFovKick;
    [SerializeField] private FOVKick m_FovKick = new FOVKick();
    [SerializeField] private bool m_UseHeadBob;
    [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
    [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
    [SerializeField] private float m_StepInterval;

    private Camera m_Camera;
    private bool m_Jump;
    private float m_YRotation;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private bool m_PreviouslyGrounded;
    private Vector3 m_OriginalCameraPosition;
    private float m_StepCycle;
    private float m_NextStep;
    private bool m_Jumping;
    private AudioSource m_AudioSource;

    [SyncVar(hook = "healthChanged")]
    public int health = 1;
    [SyncVar]
    public int id;
    private bool isDead = false;

    public GameObject laser;

    public PlayerNetwork playerNetwork  = null;

    // Use this for initialization
    private void Start()
    {

        if (isServer)
        {
            id=PlayerStructure.getInstance().addPlayer(this);
        }			

        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = this.gameObject.GetComponentInChildren<Camera>();
        m_OriginalCameraPosition = m_Camera.transform.localPosition;
        m_FovKick.Setup(m_Camera);
        m_HeadBob.Setup(m_Camera, m_StepInterval);
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_Jumping = false;
        m_AudioSource = GetComponent<AudioSource>();
        m_MouseLook.Init(transform, m_Camera.transform);
    }


	public override void OnStartAuthority()
	{
        m_Camera = this.gameObject.GetComponentInChildren<Camera>();
        m_Camera.enabled = true;
	}

    [Command]
    private void CmdkillPlayer(int id)
    {
        //playerNetwork.CmdIncrementKills();

        Player player = PlayerStructure.getInstance().getPlayer(id);
        player.health = 0;

        //player.playerNetwork.CmdIncrementDeaths();
    }

    private void healthChanged(int newHp)
    {
        health = newHp;
        if (health <= 0)
        {
            this.Die();
        }
    }

    public void Die()
    {
        isDead = true;

        EventsManager.TriggerEvent(EventsManager.Events.somebodyDied);

        this.GetComponentInChildren<MeshRenderer>().enabled = false;
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;

        Invoke("Respawn", 3f);

        if (hasAuthority)
            EventsManager.TriggerEvent(EventsManager.Events.died);
    }

    public void Respawn()
    {
        isDead = false;

        if (hasAuthority)
        {
            Vector3 position = new Vector3(0, 3, 0);
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            EventsManager.TriggerEvent(EventsManager.Events.respawned);
        }

        this.GetComponentInChildren<MeshRenderer>().enabled = true;
        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        health = 1;

    }

    bool shoot()
    {
        RaycastHit hitInfo;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.point == Vector3.zero)
            hitInfo.point = ray.origin + (100 * ray.direction);

        CmddrawLaser(ray.origin, hitInfo.point);
        drawLaser(ray.origin, hitInfo.point);


        if (hitInfo.collider && hitInfo.collider.tag == "Player")
        {
            Player p = (Player)hitInfo.collider.gameObject.GetComponent(typeof(Player));
            CmdkillPlayer(p.id);
            return true;
        }
        return false;
    }

    [Command]
    void CmddrawLaser(Vector3 start, Vector3 end)
    {
        RpcdrawLaser(start, end);
    }

    [ClientRpc]
    void RpcdrawLaser(Vector3 start, Vector3 end)
    {
        if (!hasAuthority)
            drawLaser(start, end);
    }

    void drawLaser(Vector3 start, Vector3 end)
    {
        float duration = 1;

        GameObject myLine = Instantiate(laser);
        myLine.transform.position = start;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        //NetworkServer.Spawn(myLine);

        GameObject.Destroy(myLine, duration);
    }


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
           Die();

        if (isDead)
            return;

		if (!hasAuthority)
        {
			return;
        }

        RotateView();

        // the jump state needs to read here to make sure it is not missed
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            StartCoroutine(m_JumpBob.DoBobCycle());
            m_MoveDir.y = 0f;
            m_Jumping = false;
        }
        if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            m_MoveDir.y = 0f;
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;
    }

    private void FixedUpdate()
    {

        float speed;
        GetInput(out speed);

		if (isDead || !hasAuthority)
            return;

        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            shoot(/*gameObject.transform.TransformVector(Vector3.zero), new Vector3(yaw / 90, -pitch / 90,0)*/);
        }


        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;

            if (m_Jump)
            {
                m_MoveDir.y = m_JumpSpeed;
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed);

        m_MouseLook.UpdateCursorLock();
    }

    private void ProgressStepCycle(float speed)
    {

        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;
    }

    private void UpdateCameraPosition(float speed)
    {

		if (isDead || !hasAuthority)
			return;

        Vector3 newCameraPosition;
        if (!m_UseHeadBob)
        {
            return;
        }
        if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
        {
            m_Camera.transform.localPosition =
                m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                  (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
        }
        else
        {
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
        }
        m_Camera.transform.localPosition = newCameraPosition;
    }


    private void GetInput(out float speed)
    {

        // Read input
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
        // On standalone builds, walk/run speed is modified by a key press.
        // keep track of whether or not the character is walking or running
        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        // set the desired speed to be walking or running
        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }

        // handle speed change to give an fov kick
        // only if the player is going to a run, is running and the fovkick is to be used
        if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
        {
            StopAllCoroutines();
            StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
        }
    }


    private void RotateView()
    {

        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
