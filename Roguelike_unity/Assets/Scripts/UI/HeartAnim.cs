using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartAnim : MonoBehaviour
{
    public Sprite[] sprites;
    public int spritePerFrame = 6;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private int index = 0;
    private Image image;
    private float frame = 0;

    bool GoToHalf;
    bool GoToEmpty;
    bool GoToFull;

    public enum HeartStatus 
        {
        Full,
        Half,
        Empty
        }

    public HeartStatus HeartFilled = HeartStatus.Full;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void EmptyHeart()
    {
        GoToHalf = false;
        GoToFull = false;
        GoToEmpty = true;
        HeartFilled = HeartStatus.Empty;
    }
    public void HalfHeart()
    {
        GoToHalf = true;
        GoToFull = false;
        GoToEmpty = false;
        HeartFilled = HeartStatus.Half;
    }
    public void FullHeart()
    {
        GoToHalf = false;
        GoToFull = true;
        GoToEmpty = false;
        HeartFilled = HeartStatus.Full;
    }

    void Update()
    {
        if (GoToHalf)
        {
            if (index < 3)
            {
                if (frame < spritePerFrame)
                {
                    frame += 60 * Time.deltaTime;
                    if (frame >= spritePerFrame)
                    {
                        index++;
                        image.sprite = sprites[index];
                        frame = 0;
                    }
                }
            }
            if(index>3)
            {
                if (frame < spritePerFrame)
                {
                    frame += 60 * Time.deltaTime;
                    if (frame >= spritePerFrame)
                    {
                        index--;
                        image.sprite = sprites[index];
                        frame = 0;
                    }
                }
            }
        }
        if (GoToEmpty)
        {
            if (index < 6)
            {
                if (frame < spritePerFrame)
                {
                    frame += 60 * Time.deltaTime;
                    if (frame >= spritePerFrame)
                    {
                        index++;
                        image.sprite = sprites[index];
                        frame = 0;
                    }
                }
            }
        }
        if (GoToFull)
        {
            if (index > 0)
            {
                if (frame < spritePerFrame)
                {
                    frame += 60 * Time.deltaTime;
                    if (frame >= spritePerFrame)
                    {
                        index--;
                        image.sprite = sprites[index];
                        frame = 0;
                    }
                }
            }
        }    
    }
}
