using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    public void Hit()
    {
        health -= 1;
        Debug.Log("Health: " + health);
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, (float)health) ;
        if (health == 0)
        {
            Debug.Break();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
