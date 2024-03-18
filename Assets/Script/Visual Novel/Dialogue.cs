using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name; 
    public string icon;
    public string[] text;
    public Choice[] choices;
    public string next;

    [System.Serializable]
    public class Choice
    {
        public string text; 
        public string next; 
        public string show_only_if; 
    }
}