using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreboardController {
    public static Player[] getRankings()
    {
        PlayerStructure structure = PlayerStructure.getInstance();
        Player[] liste = (Player[])structure.getListe().Clone();

        Array.Sort(liste);

        return liste;
    }
}
