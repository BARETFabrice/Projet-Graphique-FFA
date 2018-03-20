using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreboardController {
    public static Player[] getRankings()
    {
        PlayerStructure structure = PlayerStructure.getInstance();
        Player[] liste = structure.getListe();

        Array.Sort(liste);

        return liste;
    }
}
