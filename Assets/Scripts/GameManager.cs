using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    float timeLeft;
    static GameManager instance=null;

    public static GameManager getInstance()
    {
        return instance;
    }

    public string getTimer()
    {
        uint time = 0;

        if(timeLeft>0)
            time = (uint)timeLeft;

        uint minutes = time / 60;
        uint seconds = time - (minutes * 60);

        string timer = "" + minutes + ":";

        if (seconds < 10)
            timer += '0';

        timer += seconds;

        return timer;
    }

    [Command]
    void CmdSyncTimeLeft()
    {
        RpcSyncTimeLeft(timeLeft);
    }

    [ClientRpc]
    void RpcSyncTimeLeft(float timeLeft)
    {
        setTime(timeLeft);
    }

    void setTime(float time)
    {
        timeLeft = time;
    }

    // Use this for initialization
    void Start () {
        instance = this;

        setTime(600);

        if (isServer)
            InvokeRepeating("CmdSyncTimeLeft", 1.0f, 50F);
    }

    [Command]
    void CmdEndGame()
    {

    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;

        if (isServer && timeLeft < 0)
            CmdEndGame();
    }
}
