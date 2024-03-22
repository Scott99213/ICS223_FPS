using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int health;
    private int maxHealth = 5;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.PICKUP_HEALTH, this.OnPickupHealth);
    }
    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.PICKUP_HEALTH, this.OnPickupHealth);
    }

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
            //Debug.Break();
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickupHealth(int healthAdded)
    {
        health += healthAdded;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, health);
    }
}
