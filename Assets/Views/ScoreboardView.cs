using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardView : MonoBehaviour {

    public void Start()
    {
        // La vue écoute les différents événements qui ont un impacte sur cette dernière et agit en conséquence
        EventsManager.AddListener(EventsManager.Events.OpenScoreboard, OnOpenScoreboard);
        EventsManager.AddListener(EventsManager.Events.CloseScoreboard, OnCloseScoreboard);
    }

    public void OnDisable()
    {
        EventsManager.RemoveListener(EventsManager.Events.CloseScoreboard, OnCloseScoreboard);
        EventsManager.AddListener(EventsManager.Events.OpenScoreboard, OnOpenScoreboard);
    }

    public void OnOpenScoreboard() {
        Debug.Log("open");
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void OnCloseScoreboard()
    {
        Debug.Log("close");
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
