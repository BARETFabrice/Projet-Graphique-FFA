using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public Transform pauseMenuCanvas;
    private bool pauseMenuOpened = false;

	void Update ()
    {
        if(Input.GetKey(KeyCode.P))
        {

            if (pauseMenuCanvas.gameObject.activeInHierarchy == true)
            {
                EventsManager.TriggerEvent(EventsManager.Events.ClosePauseMenu);
                pauseMenuOpened = false;
            }
            else
            {
                EventsManager.TriggerEvent(EventsManager.Events.OpenPauseMenu);
                pauseMenuOpened = true;
            }
        }
    }
}
