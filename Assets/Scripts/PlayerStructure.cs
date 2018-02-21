using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStructure
{
    private static PlayerStructure instance=null;
    private List<Player> liste;

    public static PlayerStructure getInstance()
    {
        if (instance == null)
            instance = new PlayerStructure();

        return instance;
    }

    private PlayerStructure()
    {
        liste = new List<Player>();
    }

    public int addPlayer(Player p)
    {
        liste.Add(p);

        Debug.Log("added " + (liste.Count-1));

        return liste.Count-1;
    }

    public Player getPlayer(int id)
    {
        if (id < 0 || liste.Count < id)
            return null;

        Debug.Log("returned " + id);

        Player p = liste[id];

        return p;
    }
}
