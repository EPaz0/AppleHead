using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameManagerScript gameManager;

    public int health = 100;
    
    public GameObject deathEffect;
    

    public void TakeDamage(int damage)
    {
             Debug.Log("Enemy takes damage: " + damage); // Debug log
        health -= damage;
        Debug.Log("Enemy health: " + health); // Debug log
        //health -= damage;
        
        if(health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        //Uncomment later when have deatheeffects
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        gameManager.gameOver(); // to load game over screen - will remove when boss is implemented

    }
}
