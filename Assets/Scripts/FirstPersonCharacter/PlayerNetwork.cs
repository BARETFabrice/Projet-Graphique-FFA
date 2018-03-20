﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerNetwork : NetworkBehaviour {

    public GameObject playerH;
    private GameObject playerUnit;
    private Playerv2 player;

    // Use this for initialization
    void Start() {
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
        playerUnit = Instantiate(playerH);
        player = playerUnit.GetComponent<Playerv2>();
        player.Respawn();
        NetworkServer.SpawnWithClientAuthority (playerUnit, connectionToClient);
	}
}
