using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    public int damage = 1;

    public GameObject player;
    public GameObject boss;
    public float moveSpeed = 5f;

    float px;
    float bx;
    float dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.Find("ShooterBanana");

        px = player.transform.position.x;
        bx = boss.transform.position.x;
        dir = 1;
        if (px > bx) {
            dir = -1;
        }
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.fixedDeltaTime * dir;
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