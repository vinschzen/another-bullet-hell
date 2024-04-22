using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkSurroundPhaseTwo : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyBehavior enemy;

    private bool triggered = false;
    void Start()
    {
        this.enemy = GameObject.Find("Enemy").GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enemy.getHealth() <= 50 && !triggered)
        {
            transform.GetComponent<ForkSurround>().Duration = transform.GetComponent<ForkSurround>().Duration * 6;
            transform.GetComponent<ForkSurround>().Cooldown = transform.GetComponent<ForkSurround>().Cooldown * 2.5f;
            triggered = true;
        }
    }
}
