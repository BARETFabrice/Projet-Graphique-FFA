using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreboardController {
    public static PlayerNetwork[] getRankings()
    {
        PlayerNetwork[] rankings = new PlayerNetwork[10];

        for (int i = 0; i < 10; i++)
        {
            Player player = PlayerStructure.getInstance().getPlayer(i);

            if (player == null)
                continue;

            //int kills = player.CmdGetKills();
            //int deaths = player.CmdGetDeaths();
            //string name = "" + i;

            //Debug.Log(name + " " + kills + " " + deaths);
        }

        return rankings;
    }
}
