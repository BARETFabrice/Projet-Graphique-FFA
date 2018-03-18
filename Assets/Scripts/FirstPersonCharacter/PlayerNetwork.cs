using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {

    public GameObject playerH;
    private GameObject playerUnit;
    private Player player;

    [SyncVar]
    private int kills;
    [SyncVar]
    private int deaths;

    [Command]
    public void CmdIncrementKills()
    {
        kills++;
    }

    [Command]
    public void CmdIncrementDeaths()
    {
        deaths++;
    }

    public int getKills()
    { return kills; }

    public int getDeaths()
    { return deaths; }

    // Use this for initialization
    void Start() {
        //if (isServer && isLocalPlayer)
        //    NetworkManagerFFA.instance.ServerChangeScene("Game");



        if (isLocalPlayer)
            CmdSpawnMyUnit();

        EventsManager.AddListener(EventsManager.Events.gameEnded, destroyPhysicalPlayer);
    }

    private void OnDestroy()
    {
        EventsManager.RemoveListener(EventsManager.Events.gameEnded, destroyPhysicalPlayer);
    }

    void destroyPhysicalPlayer()
    {
        GameObject.Destroy(playerUnit);
    }

	[Command]
	void CmdSpawnMyUnit()
	{
        kills = 0;
        deaths = 0;

        playerUnit = Instantiate(playerH);
        player = playerUnit.GetComponent<Player>();
        player.Respawn();
        player.setPlayerNetwork(this);
        NetworkServer.SpawnWithClientAuthority (playerUnit, connectionToClient);
	}
}
