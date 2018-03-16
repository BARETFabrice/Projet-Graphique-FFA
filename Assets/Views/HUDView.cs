using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour {

    public Texture2D crosshairImage;
    public Text timer;
    public GameObject deathMessage;

    void OnGUI()
    {
        if(!deathMessage.activeSelf)
            drawCrosshair();
    }

    void drawCrosshair()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }

    void drawHud()
    {
        timer.text=HUDController.getTimer();
    }

    public void showDeathMessage()
    {
        deathMessage.SetActive(true);
    }

    public void hideDeathMessage()
    {
        deathMessage.SetActive(false);
    }


    // Use this for initialization
    void Start () {
        InvokeRepeating("drawHud",0.1F,0.5F);

        EventsManager.AddListener(EventsManager.Events.died, showDeathMessage);
        EventsManager.AddListener(EventsManager.Events.respawned, hideDeathMessage);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
