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

    public int damage = 1;
    public GameObject player;

    private Rigidbody2D rb;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Player");
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

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
        Debug.Log("Bullet hit: " + hitInfo.name); 
        var layerMask = hitInfo.gameObject.layer;
        Debug.Log(layerMask);
        if(layerMask == 6)
        {
            Debug.Log(player);
            player.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    
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
