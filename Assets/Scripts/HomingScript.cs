using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public GameObject explosion;

    public int damage = 15;

    private Rigidbody2D rb;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("Target not found. Make sure the player object has the 'Player' tag.");
        }
    }

    // Update is called
    // once per frame
    void FixedUpdate(){

       
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            // not working?
            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            // will go towards us
            rb.angularVelocity = -rotateAmount * rotateSpeed;

            // set the point to the upwards direction
            rb.velocity = transform.up * speed;
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        // unity will call when enter trigger

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage); // Apply damage to the player
            }

            //Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject); // Destroy the missile
        }
        else
        {
            //Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject); // Destroy the missile if it hits anything else
        }



    }
    /*public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject); // Replace with your health reduction logic if necessary
    }*/
    void Update()
    {
        
    }
}
