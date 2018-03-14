using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        //if (isServer && isLocalPlayer)
        //    NetworkManagerFFA.instance.ServerChangeScene("Game");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
