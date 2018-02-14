using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {


	// Use this for initialization
	void Start () {
		if (isLocalPlayer == false) {
			return;
		}
			
		//UnitInstance = Instantiate (PlayerUnitPrefab);
		CmdSpawnMyUnit();
	}

	public GameObject PlayerUnitPrefab;

	// Update is called once per frame
	void Update () {
		if (isLocalPlayer == false) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdMoveUnitUp ();
		}
	}

	GameObject myPlayerUnit;

	[Command]
	void CmdSpawnMyUnit(){
		GameObject go = Instantiate (PlayerUnitPrefab);

		myPlayerUnit = go;

		NetworkServer.Spawn (go);
	}

	[Command]
	void CmdMoveUnitUp()
	{
		if (myPlayerUnit == null) {
			return;
		}
		myPlayerUnit.transform.Translate (0, 1, 0);
	}

	/*void OnDestroy(){
		Object.Destroy (UnitInstance);
	}*/
}
