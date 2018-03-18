using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStructure
{
    private static PlayerStructure instance=null;
    private Player[] liste;

    public static PlayerStructure getInstance()
    {
        if (instance == null)
            PlayerStructure.newInstance();

        return instance;
    }

    public static PlayerStructure newInstance()
    {
        instance = new PlayerStructure();

        return instance;
    }

    private PlayerStructure()
    {
        liste = new Player[10];
        
    }

    public int addPlayer(Player p)
    {
        int position = 0;

        while (liste[position])
        {
            if (position < 9)
                position++;
            else
                return -1;
        }

        liste[position] = p;

        return position;
    }

    public Player getPlayer(int id)
    {
        if (id < 0 || liste.Length < id)
            return null;

        Player p = liste[id];

        return p;
    }
}
