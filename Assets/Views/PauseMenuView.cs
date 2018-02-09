using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuView : MonoBehaviour
{
    private bool pauseMenuEnabled = false;

    public void OnEnable()
    {
        //gestion de la sourie a deplacer aprés
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;

        gameObject.GetComponent<CanvasGroup>().alpha = 0f;

        EventsManager.AddListener(EventsManager.Events.PPressed, OnTogglePauseMenu);
        EventsManager.AddListener(EventsManager.Events.TogglePauseMenu, OnTogglePauseMenu);
    }

    public void OnDisable()
    {
        EventsManager.RemoveListener(EventsManager.Events.PPressed, OnTogglePauseMenu);
        EventsManager.RemoveListener(EventsManager.Events.TogglePauseMenu, OnTogglePauseMenu);
    }

    public void OnTogglePauseMenu()
    {
        //gameObject.GetComponent<CanvasGroup>().alpha = 1f - gameObject.GetComponent<CanvasGroup>().alpha;

        if(!pauseMenuEnabled)
        {
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().alpha = 1f;
            pauseMenuEnabled = true;
            
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            pauseMenuEnabled = false;

            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        }
    }

    public void OnButtonQuitClicked()
    {
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        PauseMenuController.OnButtonQuitClicked();
    }

    public void OnButtonExitClicked()
    {
        PauseMenuController.OnButtonExitClicked();
    }
}
