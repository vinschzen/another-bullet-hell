using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotNewGamePlus : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerData data;
    void Start()
    {
        data = SaveManager.Instance.CurrentSave;
        if (data.newgameplus < 1)
        {
            transform.position = new Vector3(0, 1000, 1000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
