using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int difficulty;
    public int level;
    public int exp;
    public float playtime;
    public List<string> progress; 
    public List<Stat> stats; 
    public int newgameplus;
    public int saveslot;

    [System.Serializable]
    public struct Stat
    {
        public string name;
        public int value;
    }

    public PlayerData(string name,int difficulty, int saveslot)
    {
        this.name = name;
        this.difficulty = difficulty;
        this.level = 1;
        this.exp = 0;
        this.playtime = 0f;
        this.progress = new List<string>();
        this.progress.Add("1");
        this.stats = new List<Stat>
        {
            new Stat { name = "attack", value = 1 },
            new Stat { name = "defense", value = 1 },
            new Stat { name = "health", value = 3 }
        };
        this.newgameplus = 0;
        this.saveslot = saveslot;
    }
}
