using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class Playerv2 : NetworkBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        [SyncVar(hook = "healthChanged")]
        public int health = 1;
        [SyncVar]
        public int id;
        private bool isDead = false;

        private Camera m_Camera;
        public GameObject laser;
        public PlayerNetwork playerNetwork = null;

        private void Start()
        {
            m_Camera = this.gameObject.GetComponentInChildren<Camera>();

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        public override void OnStartAuthority()
        {
            m_Camera = this.gameObject.GetComponentInChildren<Camera>();
            m_Camera.enabled = true;
        }

        [Command]
        private void CmdkillPlayer(int id)
        {
            playerNetwork.CmdIncrementKills();

            Player player = PlayerStructure.getInstance().getPlayer(id);
            player.health = 0;

            player.playerNetwork.CmdIncrementDeaths();
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

            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (isDead || !hasAuthority)
                return;
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        //if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;

            if (Input.GetMouseButtonDown(0) && !isDead)
            {
                shoot(/*gameObject.transform.TransformVector(Vector3.zero), new Vector3(yaw / 90, -pitch / 90,0)*/);
            }
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
