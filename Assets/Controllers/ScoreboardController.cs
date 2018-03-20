using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public static class ScoreboardController {
    public static Playerv2[] getRankings()
    {
        PlayerStructure structure = PlayerStructure.getInstance();
        Playerv2[] liste = (Playerv2[])structure.getListe().Clone();

        Array.Sort(liste);

        return liste;
    }
}
