using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour {

    private Rigidbody body;
    private Camera cam;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public GameObject laser;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    bool shoot(/*Vector3 origin, Vector3 direction*/)
    {
        RaycastHit hitInfo;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.point == Vector3.zero)
            hitInfo.point = ray.origin + (100 * ray.direction);

        if(hitInfo.collider!=null)
            Debug.Log(hitInfo.collider.gameObject.name);

        CmddrawLaser(ray.origin,hitInfo.point);

        if (hitInfo.collider && hitInfo.collider.gameObject.tag == "Player")
        {
            Debug.Log("kill");
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
    void RpcdrawLaser(Vector3 start, Vector3 end) {
        float duration = 30;

        GameObject myLine = Instantiate(laser);
        myLine.transform.position = start;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        //NetworkServer.Spawn(myLine);

        GameObject.Destroy(myLine, duration);
    }

    // Use this for initialization
    void Start () {
		cam = GetComponent<Camera> ();
		body = GetComponent<Rigidbody>();
		Invoke ("ReactivateCam", 0.1f);
	}

	public void ReactivateCam(){
		if ( hasAuthority == false) {
			return;
		}
        cam.enabled = false;
        cam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if ( hasAuthority == false) {
			return;
		}
        Vector3 velocity = body.velocity;

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (yaw > 360)
            yaw -= 360;
        if (yaw < 0)
            yaw += 360;

        if (pitch > 90)
            pitch = 90;
        if (pitch < -90)
            pitch = -90;

        cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetMouseButtonDown(0))
        {
            shoot(/*gameObject.transform.TransformVector(Vector3.zero), new Vector3(yaw / 90, -pitch / 90,0)*/);
        }
             

		if (Input.GetKeyDown (KeyCode.Space)) {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y+8, body.velocity.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector3(5, body.velocity.y, body.velocity.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector3(-5, body.velocity.y, body.velocity.z);
        }
        else
        {
            body.velocity = new Vector3(0, body.velocity.y, body.velocity.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, 5);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, -5);
        }
        else
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, 0);
        }
    }
}
