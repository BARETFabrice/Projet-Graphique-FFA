using UnityEngine;
using UnityEngine.UI;

public class NetworkMenuView : MonoBehaviour
{
	private NetworkMenuController myController;

	private void Awake()
	{
		myController = GetComponent<NetworkMenuController>();
	}

    public void OnButtonHostClicked()
    {
		myController.HostGame();
    }

	public void OnButtonJoinClicked()
	{
		myController.JoinGame();
	}

	public void OnButtonServerListClicked()
	{
		myController.LoadListServer();
	}

	public void onButtonReturnClicked()
	{
		myController.LoadMainMenuScene();
	}
}
