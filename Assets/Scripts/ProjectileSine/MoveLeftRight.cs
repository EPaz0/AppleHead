using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    public int damage = 1;

    public GameObject player;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.fixedDeltaTime;
        transform.position = pos;
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {

       // Debug.Log("Bullet hit: " + hitInfo.name); 
        var layerMask = hitInfo.gameObject.layer;
        //Debug.Log(layerMask);
        if(layerMask == 6)
        {
            Debug.Log("GETTING HIT");
            player.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(layerMask == 12)
        {
            Destroy(gameObject);
        }

    }
}