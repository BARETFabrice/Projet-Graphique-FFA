using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkMenuController : MonoBehaviour
{
    public void Start()
    {
        NetworkManagerFFA.instance.StartMatchMaker();
    }
    public void HostGame()
    {
		
		NetworkManagerFFA.instance.matchMaker.CreateMatch("test", 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

	public void JoinGame()
	{
		NetworkManagerFFA.instance.matchMaker.ListMatches(0, 10, "test", true, 0, 0, OnInternetMatchList);
	}
		
	public void LoadMainMenuScene()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Create match succeeded");

			MatchInfo hostInfo = matchInfo;
			NetworkServer.Listen(hostInfo, 9000);

			NetworkManagerFFA.instance.StartHost(hostInfo);

            //SceneManager.LoadScene("Game");
        }
		else
		{
			Debug.LogError("Create match failed");
		}
	}

	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		Debug.Log (success);
		if (success)
		{
			if (matches.Count != 0)
			{
				Debug.Log("A list of matches was returned");

				//join the last server (just in case there are two...)
				NetworkManagerFFA.instance.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
			}
			else
			{
				Debug.Log("No matches in requested room!");
			}
		}
		else
		{
			Debug.LogError("Couldn't connect to match maker");
		}
	}

	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			//Debug.Log("Able to join a match");

			MatchInfo hostInfo = matchInfo;
			NetworkManagerFFA.instance.StartClient(hostInfo);
			//SceneManager.LoadScene("Game");
		}
		else
		{
			Debug.LogError("Join match failed");
		}
	}
}
