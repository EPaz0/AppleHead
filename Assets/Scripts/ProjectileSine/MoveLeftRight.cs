using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    public int damage = 1;

    public Player player;
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.fixedDeltaTime;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}