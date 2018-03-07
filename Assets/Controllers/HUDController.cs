using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HUDController {

    static private GameManager gm=null;

    public static string getTimer()
    {
        string timer="";

        if (gm == null)
            gm = GameManager.getInstance();

        if (gm != null)
            timer = gm.getTimer();

        return timer;
    }
}
