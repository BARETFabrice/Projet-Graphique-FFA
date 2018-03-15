using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {

	public GameObject playerH;
	private GameObject playerUnit;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        //if (isServer && isLocalPlayer)
        //    NetworkManagerFFA.instance.ServerChangeScene("Game");
		CmdSpawnMyUnit();
    }

	[Command]
	void CmdSpawnMyUnit()
	{
		GameObject p = Instantiate(playerH);
		playerUnit = p;
		NetworkServer.SpawnWithClientAuthority (playerUnit, connectionToClient);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
