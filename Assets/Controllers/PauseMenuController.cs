using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public Transform pauseMenuCanvas;
    private bool pauseMenuOpened = false;

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            mettreEnPauseLeJeu();
        }
    }

    public void mettreEnPauseLeJeu()
    {
        EventsManager.TriggerEvent(EventsManager.Events.OpenPauseMenu);
        pauseMenuOpened = true;
    }

    public void reprendreLeJeu()
    {
        EventsManager.TriggerEvent(EventsManager.Events.ClosePauseMenu);
        pauseMenuOpened = false;
    }
}
