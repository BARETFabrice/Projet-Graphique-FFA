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
			
		CmdSpawnMyUnit();
	}

	public GameObject PlayerUnitPrefab;

	// Update is called once per frame
	void Update () {
	}

	GameObject myPlayerUnit;

	[Command]
	void CmdSpawnMyUnit(){
		//Risque d'instancier la caméra avant de donner l'authority
        myPlayerUnit = Instantiate (PlayerUnitPrefab);
		//go.GetComponent<NetworkIdentity> ().AssignClientAuthority (connectionToClient);

		NetworkServer.SpawnWithClientAuthority (myPlayerUnit, connectionToClient);
	}
}
