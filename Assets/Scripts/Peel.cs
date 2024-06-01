using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peel : MonoBehaviour
{
	[Header("Variables To Destroy Peel")]

    public int damage = 1;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        StartCoroutine(DestroyAfterTime(10f));
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
       
    
    }


    // Coroutine to destroy the peel after a specified time
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
