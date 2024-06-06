using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peel : MonoBehaviour
{
	[Header("Variables To Destroy Peel")]

    public int damage = 1;
    public GameObject player;
    // Start is called before the first frame update
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    
    public float speed;
    private bool grounded;

    void Awake()
    {
        player = GameObject.Find("Player");
        StartCoroutine(DestroyAfterTime(10f));
    }
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointA.transform;
    }

    void FixedUpdate(){
        CheckGround();
    }

    void Update(){
        Vector2 point = currentPoint.position - transform.position;
        Physics2D.IgnoreLayerCollision(11, 8, true);

        Physics2D.IgnoreLayerCollision(11, 10, true);
        if (grounded == true){
            anim.SetBool("isGrounded", true);
            enemyPatrol();
        }
        else{
            anim.SetBool("isGrounded", false); // still falling
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
    
        //Debug.Log("Bullet hit: " + hitInfo.name); 
        var layerMask = hitInfo.gameObject.layer;
       // Debug.Log(layerMask);
        if(layerMask == 6)
        {
            //Debug.Log(player);
            player.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hitInfo.CompareTag("Bullet"))
        {
            Destroy(gameObject); // Destroy the peel if hit by a bullet
            Destroy(hitInfo.gameObject); // Optionally destroy the bullet as well
        }
        if (grounded == true){
            // grounded = true; // when the banana collides w ground then allow running
            enemyPatrol();
          //Debug.Log("BANANA IS GROUNDED");
        }


    }


    // Coroutine to destroy the peel after a specified time
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void enemyPatrol(){
        Physics2D.IgnoreLayerCollision(11, 11, true);
        Physics2D.IgnoreLayerCollision(11, 8, true);


        if (currentPoint == pointB.transform){
            rb.velocity = new Vector2(speed, 0);
        }
        else{
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform){
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform){
            flip();
            currentPoint = pointB.transform;
        }
    }

    void CheckGround(){
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length >0;
    }

    private void flip(){
        Vector3 localScale = transform.localScale;
        localScale.x  *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizomos(){
        // for debugging purposes to view pointA and pointB
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
