using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MainMenuController
{
    // Exemple de variable utilisée pour stocker la liste des serveurs
    // public List<Server> serversList;



    public static void LoadSinglePlayerScene()
    {
        // Exécution du traitement
        SceneManager.LoadScene("skdjfhsdkf");
    }

    public static void LoadServers()
    {
        // TODO Aller chercher la liste des serveurs et les stocker dans une variable (serversList)

        // Déclanche un événement afin que tous ceux qui y ont souscrit agissent en conséquence
        EventsManager.TriggerEvent(EventsManager.Events.ServersLoaded);
    }
}
