using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardView : MonoBehaviour {

    CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        // La vue écoute les différents événements qui ont un impacte sur cette dernière et agit en conséquence
        EventsManager.AddListener(EventsManager.Events.TabPressed, OnOpenScoreboard);
        EventsManager.AddListener(EventsManager.Events.TabReleased, OnCloseScoreboard);
    }

    public void OnDestroy()
    {
        EventsManager.RemoveListener(EventsManager.Events.TabPressed, OnOpenScoreboard);
        EventsManager.AddListener(EventsManager.Events.TabReleased, OnCloseScoreboard);
    }

    public void OnOpenScoreboard() {
        canvasGroup.alpha = 1f;
    }

    public void OnCloseScoreboard()
    {
        canvasGroup.alpha = 0f;
    }
}
