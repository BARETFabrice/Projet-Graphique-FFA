using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerFFA : NetworkManager
{
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        if(GameManager.getInstance() != null)
            GameManager.getInstance().SyncVarTimeLeft();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnServerAddPlayer");

        base.OnServerAddPlayer(conn, playerControllerId);

        NetworkServer.SpawnObjects();
    }
}
