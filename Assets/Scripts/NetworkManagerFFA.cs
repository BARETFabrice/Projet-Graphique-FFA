using System.Collections;
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

        isServer = true;
    }

    public void endGame()
    {
        if (!isServer)
            return;

        instance = null;
        this.ServerChangeScene("MainMenu");
        Network.Disconnect(0);
        this.StopServer();
        NetworkManagerFFA.Shutdown();
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            Destroy(gameObject);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        if(GameManager.getInstance() != null)
            GameManager.getInstance().SyncVarTimeLeft();
    }
}
