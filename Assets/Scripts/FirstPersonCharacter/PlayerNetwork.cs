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
		CmdSpawnMyUnit();
    }

	[Command]
	void CmdSpawnMyUnit()
	{
        playerUnit = Instantiate(playerH);
        player = playerUnit.GetComponent<Player>();
        player.setPlayerNetwork(this);
        Respawn();
        NetworkServer.SpawnWithClientAuthority (playerUnit, connectionToClient);
	}

    public void died()
    {
        Invoke("Respawn", 3f);
    }

    private void Respawn()
    {
        player.Respawn(new Vector3(0, 3, 0));
    }

	// Update is called once per frame
	void Update () {
		
	}
}
