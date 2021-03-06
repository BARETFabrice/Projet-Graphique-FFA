﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManagerFFA : NetworkManager
{
    public static NetworkManagerFFA instance;
    private bool isServer;

    private void Awake()
    {
        instance = this;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();


        PlayerStructure.newInstance();

        isServer = true;
    }

    public void endGame()
    {
        if (!isServer)
            return;
        
        instance = null;

        for (int i = Network.connections.Length-1 ; i > 1; i--)
        {
            Network.CloseConnection(Network.connections[i], false);
        }
        Network.Disconnect(0);
        this.StopMatchMaker();
        this.StopAllCoroutines();
        GameObject.Destroy(gameObject, 0);
        this.StopServer();
        this.StopHost();

    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        if(GameManager.getInstance() != null)
            GameManager.getInstance().SyncVarTimeLeft();
    }
}
