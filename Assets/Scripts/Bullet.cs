using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 1;
    public GameObject impactEffect; //For bullet impact animation

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
       //hitInfo.GetComponent<Enemy>();
      // Debug.Log("Bullet hit: " + hitInfo.name); 
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        //Instantiate(imapctEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    
    }
}
