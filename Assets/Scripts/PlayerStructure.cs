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

    public Player[] getListe()
    {
        return liste;
    }

    private PlayerStructure()
    {
        liste = new Player[10];

        for(int i=0; i<10; i++)
        {
            liste[i] = null;
        }
    }

    public int addPlayer(Player p)
    {
        int position = 0;

        while (liste[position])
        {
            if (position < liste.Length-1)
                position++;
            else
                return -1;
        }

        liste[position] = p;

        return position;
    }

    public void addPlayerAtId(int id, Player p)
    {
        liste[id] = p;
    }

    public Player getPlayer(int id)
    {
        if (id < 0 || liste.Length <= id)
            return null;

        Player p = liste[id];

        return p;
    }
}
