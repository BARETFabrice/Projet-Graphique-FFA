using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsManager
{
    private static Dictionary<Events, UnityEvent> m_MyEvents = new Dictionary<Events, UnityEvent>();

    public enum Events:uint
    {
        ServersLoaded
    }

    public static void AddListener(Events a_EventId, UnityAction a_Action)
    {
        UnityEvent thisEvent = null;

        bool t_EventExists = m_MyEvents.TryGetValue(a_EventId, out thisEvent);

        if (!t_EventExists)
        {
            thisEvent = new UnityEvent();
            m_MyEvents.Add(a_EventId, thisEvent);
        }

        thisEvent.AddListener(a_Action);
    }

    public static void RemoveListener(Events a_EventId, UnityAction a_Action)
    {
        UnityEvent thisEvent = null;

        bool t_EventExists = m_MyEvents.TryGetValue(a_EventId, out thisEvent);

        if (t_EventExists)
        {
            thisEvent.RemoveListener(a_Action);
        }
    }

    public static void TriggerEvent(Events a_EventId)
    {
        UnityEvent thisEvent = null;

        bool t_EventExists = m_MyEvents.TryGetValue(a_EventId, out thisEvent);

        if (t_EventExists)
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.LogError("EventsManager tryed to trigger a non existing event");
        }
    }
}
