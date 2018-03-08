using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    [SyncVar(hook = "OnSyncedTimeLeftChanged")]
    private float syncedTimeLeft;

    private float localTimeLeft;
    private static GameManager instance=null;

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
        uint time = 0;

        if(localTimeLeft>0)
            time = (uint)localTimeLeft;

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
        Debug.Log("GameManager Awake");

        instance = this;
        localTimeLeft = 600;
    }
    // Use this for initialization
    void Start () {
        Debug.Log("GameManager Start");

        if (isServer)
        {
            InvokeRepeating("SyncVarTimeLeft", 0.1f, 10F);
        }
        else
            localTimeLeft = syncedTimeLeft;
    }

    void endGame()
    {

    }
	
	// Update is called once per frame
	void Update () {
        localTimeLeft -= Time.deltaTime;

        if (isServer && localTimeLeft < 0)
            endGame();
    }
}
