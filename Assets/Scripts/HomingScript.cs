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

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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

    void OnTriggerEnter2D() {
        // unity will call when enter trigger

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject); // will change to decrease health function



    }
    
    void Update()
    {
        
    }
}
