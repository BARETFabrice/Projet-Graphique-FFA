using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    public void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;

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
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void OnClosePauseMenu()
    {
        Debug.Log("Pause Menu Open");
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
