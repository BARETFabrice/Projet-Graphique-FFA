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
            if(!pauseMenuOpened)
            {
                EventsManager.TriggerEvent(EventsManager.Events.OpenPauseMenu);
                pauseMenuOpened = true;
            }
            else
            {
                EventsManager.TriggerEvent(EventsManager.Events.ClosePauseMenu);
                pauseMenuOpened = false;
            }
        }
    }
}
