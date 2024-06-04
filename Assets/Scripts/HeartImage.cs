using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartImage : MonoBehaviour
{
    private Image image;

    private Sprite heart_healthy;
    private Sprite heart_hurt;
    private Sprite heart_hurt2;
    [SerializeField] Sprite[] appleFace;
    [SerializeField] Sprite newSprite;
    private int health = 3;

    void Start()
    {
        health = 3;
    }

    void Update()
    {
        if (health == 3){
			newSprite = appleFace[0];
		}
        if (health == 2){
			newSprite = appleFace[1];
		}
		else if (health == 1){
			newSprite = appleFace[2];
		}
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }    
}
