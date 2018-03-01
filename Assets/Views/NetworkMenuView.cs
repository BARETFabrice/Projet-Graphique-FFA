using UnityEngine;
using UnityEngine.UI;

public class NetworkMenuView : MonoBehaviour
{
	
    public void OnButtonPlayClicked()
    {
        MainMenuController.LoadPlayScene();
    }

	public void OnButtonQuitClicked()
	{
		MainMenuController.QuitGame();
	}
}
