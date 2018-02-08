using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    public void OnEnable()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;

        EventsManager.AddListener(EventsManager.Events.PPressed, OnTogglePauseMenu);
        EventsManager.AddListener(EventsManager.Events.TogglePauseMenu, OnTogglePauseMenu);
    }

    public void OnDisable()
    {
        Debug.Log("PauseMenuView disable");
        EventsManager.RemoveListener(EventsManager.Events.PPressed, OnTogglePauseMenu);
        EventsManager.RemoveListener(EventsManager.Events.TogglePauseMenu, OnTogglePauseMenu);
    }

    public void OnTogglePauseMenu()
    {
        Debug.Log("toggle pause");
        gameObject.GetComponent<CanvasGroup>().alpha = 1f - gameObject.GetComponent<CanvasGroup>().alpha;
    }

    public void OnButtonQuitClicked()
    {
        PauseMenuController.OnButtonQuitClicked();
    }

    public void OnButtonExitClicked()
    {
        PauseMenuController.OnButtonExitClicked();
    }
}
