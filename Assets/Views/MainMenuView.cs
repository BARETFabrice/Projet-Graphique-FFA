using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
	
    public void OnButtonSinglePlayerClicked()
    {
        MainMenuController.LoadSinglePlayerScene();
    }

	public void OnButtonQuitClicked()
	{
		//Ferme l'application et Unity Editor

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
