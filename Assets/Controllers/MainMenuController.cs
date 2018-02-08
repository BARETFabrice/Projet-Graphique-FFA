using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MainMenuController
{
    public static void LoadPlayScene()
    {
        // Exécution du traitement
        SceneManager.LoadScene("alex");
    }

	public static void QuitGame()
	{
		//Ferme l'application et Unity Editor

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
