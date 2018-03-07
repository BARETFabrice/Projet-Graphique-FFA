using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkMenuController : MonoBehaviour
{
    public void HostGame()
    {
		NetworkManager.singleton.StartMatchMaker ();
		NetworkManager.singleton.matchMaker.CreateMatch("test", 4, true, "", "", "", 0, 0, OnMatchCreate);
    }

	public void JoinGame()
	{

	}

	public void LoadListServer()
	{
		
	}

	public void LoadMainMenuScene()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success) 
		{
			MatchInfo hostInfo = matchInfo;
			NetworkManager.singleton.StartClient (hostInfo);
		}
	}
}
