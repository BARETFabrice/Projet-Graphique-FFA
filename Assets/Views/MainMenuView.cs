using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    //private MainMenuController controller; // On peut faire une instance ou une classe static

    // La vue contient les références vers les éléments graphiques
    [SerializeField]
    private Button btnSinglePlayer;



    public void Start()
    {
        // La vue écoute les différents événements qui ont un impacte sur cette dernière et agit en conséquence
        EventsManager.AddListner(EventsManager.Events.ServersLoaded, OnServersLoaded);
    }

    public void OnDisable()
    {
        EventsManager.RemoveListner(EventsManager.Events.ServersLoaded, OnServersLoaded);
    }

    // La gestion des événements graphiques se fait dans la vue (ex: Button click)
    public void OnButtonSinglePlayerClicked()
    {
        Debug.Log("OnButtonSinglePlayerClicked");

        // Les effets graphiques sont exécutés dans la vue
        ChangeButtonColor(btnSinglePlayer, Color.red);

        // Le traitement se fait dans le Controller (instance ou static)
        MainMenuController.LoadSinglePlayerScene();
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

    private void ChangeButtonColor(Button btn, Color clr)
    {
        //Todo effet graphique (ex: changer la couleur)
    }
}
