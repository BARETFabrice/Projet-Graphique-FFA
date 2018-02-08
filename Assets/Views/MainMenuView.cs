using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    //private MainMenuController controller; // On peut faire une instance ou une classe static

    // La vue contient les références vers les éléments graphiques




    public void Start()
    {
        // La vue écoute les différents événements qui ont un impacte sur cette dernière et agit en conséquence
        EventsManager.AddListener(EventsManager.Events.ServersLoaded, OnServersLoaded);
    }

    public void OnDisable()
    {
        EventsManager.RemoveListener(EventsManager.Events.ServersLoaded, OnServersLoaded);
    }

    // La gestion des événements graphiques se fait dans la vue (ex: Button click)
    public void OnButtonSinglePlayerClicked()
    {

        // Le traitement se fait dans le Controller (instance ou static)
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

    public void OnButtonMultiPlayerClicked()
    {
        MainMenuController.LoadServers();
    }

    private void OnServersLoaded()
    {
        // TODO Afficher la liste des serveurs
        // ex: Foreach Server in MainMenuController.ServersList -> do Afficher bouton

    }
}
