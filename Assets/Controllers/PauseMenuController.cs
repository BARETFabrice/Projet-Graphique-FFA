using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseMenuController
{

    public static void mettreEnPauseLeJeu()
    {
        EventsManager.TriggerEvent(EventsManager.Events.TogglePauseMenu);
    }

    public static void OnButtonExitClicked()
    {
        //Ferme l'application et Unity Editor

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public static void OnButtonQuitClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
