using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
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
