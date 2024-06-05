using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image image;
    public List<Sprite> spriteChoices;

    private int counter;
    private int currentSprite = 0;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void NextSprite(int add)
    {
        counter = counter + add;
        if (counter == 1)
        {
            currentSprite++;
            counter = 0;
            if (currentSprite >= spriteChoices.Count)
            {
                currentSprite = 0;
            }
            image.sprite = spriteChoices[currentSprite];
        }
    }    
}
