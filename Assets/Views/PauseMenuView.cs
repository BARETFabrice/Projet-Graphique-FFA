using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    public Transform pauseMenuCanvas;

    public void Start()
    {
        EventsManager.AddListener(EventsManager.Events.OpenPauseMenu, OnOpenPauseMenu);
        EventsManager.AddListener(EventsManager.Events.ClosePauseMenu, OnClosePauseMenu);
    }

    public void OnDisable()
    {
        EventsManager.RemoveListener(EventsManager.Events.OpenPauseMenu, OnClosePauseMenu);
        EventsManager.RemoveListener(EventsManager.Events.ClosePauseMenu, OnClosePauseMenu);
    }

    public void OnOpenPauseMenu()
    {
        Debug.Log("Pause Menu Open");
        pauseMenuCanvas.gameObject.SetActive(true);
    }

    public void OnClosePauseMenu()
    {
        Debug.Log("Pause Menu Open");
        pauseMenuCanvas.gameObject.SetActive(false);
    }
}
