using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour {

    public Texture2D crosshairImage;
    public Text timer;

    void OnGUI()
    {
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

    // Use this for initialization
    void Start () {
        InvokeRepeating("drawHud",0.1F,0.9F);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
