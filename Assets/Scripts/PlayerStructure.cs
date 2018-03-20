﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerStructure
{
    private static PlayerStructure instance=null;
    private Playerv2[] liste;

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

    public Playerv2[] getListe()
    {
        return liste;
    }

    private PlayerStructure()
    {
        liste = new Playerv2[10];

        for(int i=0; i<10; i++)
        {
            liste[i] = null;
        }
    }

    public int addPlayer(Playerv2 p)
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

    public void addPlayerAtId(int id, Playerv2 p)
    {
        liste[id] = p;
    }

    public Playerv2 getPlayer(int id)
    {
        if (id < 0 || liste.Length <= id)
            return null;

        Playerv2 p = liste[id];

        return p;
    }
}
