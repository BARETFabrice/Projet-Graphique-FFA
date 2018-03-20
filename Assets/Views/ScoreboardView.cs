using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardView : MonoBehaviour {

    CanvasGroup canvasGroup;

    public Text[] names;
    public Text[] kills;
    public Text[] deaths;

    public void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        EmptyScoreboard();

        // La vue écoute les différents événements qui ont un impacte sur cette dernière et agit en conséquence
        EventsManager.AddListener(EventsManager.Events.TabPressed, OnOpenScoreboard);
        EventsManager.AddListener(EventsManager.Events.TabReleased, OnCloseScoreboard);
        EventsManager.AddListener(EventsManager.Events.somebodyDied, UpdateScoreboard);
    }

    private void UpdateScoreboard()
    {
        Player[] rankings = ScoreboardController.getRankings();

        for (var i = 0; i < 10; i++)
        {
            Player p = rankings[i];

            if (p == null)
            {
                Debug.Log("" + i + " = null");
                continue;
            }

            int position = 9 - i;

            names[position].text = "Player " + i;
            kills[position].text = ""+p.getKills();
            deaths[position].text = "" + p.getDeaths();
        }
    }

    private void EmptyScoreboard()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = "";
            deaths[i].text = "";
            kills[i].text = "";
        }
    }

    public void OnDestroy()
    {
        EventsManager.RemoveListener(EventsManager.Events.somebodyDied, UpdateScoreboard);
        EventsManager.RemoveListener(EventsManager.Events.TabPressed, OnOpenScoreboard);
        EventsManager.RemoveListener(EventsManager.Events.TabReleased, OnCloseScoreboard);
    }

    public void OnOpenScoreboard() {
        canvasGroup.alpha = 1f;
    }

    public void OnCloseScoreboard()
    {
        canvasGroup.alpha = 0f;
    }
}
