using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceInputListener : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            EventsManager.TriggerEvent(EventsManager.Events.TabPressed);
        else if (Input.GetKeyUp(KeyCode.Tab))
            EventsManager.TriggerEvent(EventsManager.Events.TabReleased);

        if (Input.GetKeyDown(KeyCode.P))
            EventsManager.TriggerEvent(EventsManager.Events.PPressed);
    }
}
