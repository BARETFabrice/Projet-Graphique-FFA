using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardController : MonoBehaviour {

    private bool scoreboardOpened = false;

	void Update () {
      

        if (!scoreboardOpened) {
            if (Input.GetKeyDown("tab"))
            {
                EventsManager.TriggerEvent(EventsManager.Events.OpenScoreboard);
                scoreboardOpened = true;
            }
        }
        else{
            if (Input.GetKeyUp("tab"))
            {
                EventsManager.TriggerEvent(EventsManager.Events.CloseScoreboard);
                scoreboardOpened = false;
            }
        }

    }
}
