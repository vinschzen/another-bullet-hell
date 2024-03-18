using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerProfile
{
    public int PlayerLevel { get; set; }
    public int PlayerExperience { get; set; }
    public HashSet<string> UnlockedStages { get; set; }
    public Hashtable Allocations { get; set; }

    public string PlayerName { get; set; }

    public PlayerProfile(string playerName)
    {
        PlayerName = playerName;
        PlayerLevel = 1; 
        PlayerExperience = 0; 
        UnlockedStages = new HashSet<string> { "Stage 1" };
        Allocations = new Hashtable(){
            {"Health", 3},
            {"Defense", 1},
            {"Attack", 1}
        };
    }

    public void UnlockStage(string stage)
    {
        UnlockedStages.Add(stage);
    }

    public bool IsStageUnlocked(string stage)
    {
        return UnlockedStages.Contains(stage);
    }

    public void GainExperience(int amount)
    {
        PlayerExperience += amount;
        // 100 * Current Level = EXP needed to level up
    }

    public void Allocate(string stat)
    {
        if (Allocations.ContainsKey(stat))
        {
            int current = (int) Allocations[stat]; 
            current++; 
            Allocations[stat] = current; 
        }
    }
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public static PlayerProfile FromJson(string json)
    {
        return JsonUtility.FromJson<PlayerProfile>(json);
    }
}
