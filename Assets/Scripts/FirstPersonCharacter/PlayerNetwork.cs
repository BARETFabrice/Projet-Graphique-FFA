using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {

	public GameObject playerH;
	private GameObject playerUnit;
    private Player player;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        //if (isServer && isLocalPlayer)
        //    NetworkManagerFFA.instance.ServerChangeScene("Game");
        if(isLocalPlayer)
		    CmdSpawnMyUnit();
    }

	[Command]
	void CmdSpawnMyUnit()
	{
        playerUnit = Instantiate(playerH);
        player = playerUnit.GetComponent<Player>();
        player.Respawn();
        NetworkServer.SpawnWithClientAuthority (playerUnit, connectionToClient);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
