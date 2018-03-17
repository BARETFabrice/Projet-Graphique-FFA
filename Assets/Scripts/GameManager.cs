using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    [SyncVar(hook = "OnSyncedTimeLeftChanged")]
    private float syncedTimeLeft;

    private float localTimeLeft = 0;
    private static GameManager instance=null;

    public GameObject FinalCamera;

    private void OnSyncedTimeLeftChanged(float newValue)
    {
        syncedTimeLeft = newValue;
        localTimeLeft = syncedTimeLeft;
    }

    public void SyncVarTimeLeft()
    {
        syncedTimeLeft = localTimeLeft;
    }

    public static GameManager getInstance()
    {
        return instance;
    }

    public string getTimer()
    {
        if (localTimeLeft <= 0)
            return "";

        uint time = (uint)localTimeLeft;

        uint minutes = time / 60;
        uint seconds = time - (minutes * 60);

        string timer = "" + minutes + ":";

        if (seconds < 10)
            timer += '0';

        timer += seconds;

        return timer;
    }

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {

        if (isServer)
        {
            localTimeLeft = 600;
            InvokeRepeating("SyncVarTimeLeft", 0.1f, 10F);
        }
        else
            localTimeLeft = syncedTimeLeft;
    }

    void closeServer()
    {

    }

    void endGame()
    {
        FinalCamera.SetActive(true);
        gameObject.GetComponent<InterfaceInputListener>().enabled=false;
        EventsManager.TriggerEvent(EventsManager.Events.TabPressed);

        Invoke("closeServer",6F);

        EventsManager.TriggerEvent(EventsManager.Events.gameEnded);
        RpcTriggerEndGame();
    }

    [ClientRpc]
    void RpcTriggerEndGame()
    {
        EventsManager.TriggerEvent(EventsManager.Events.gameEnded);
    }
	
	// Update is called once per frame
	void Update () {
        localTimeLeft -= Time.deltaTime;

        if (isServer && localTimeLeft < 0)
            endGame();
    }
}
