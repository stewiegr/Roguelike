using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavRedir : MonoBehaviour
{
    Vector2 Pos;
    HomingAI sub;
    float forceX = 0;
    float forceY = 0;
    float pulse = 20;
    Collider2D MyTrigger;
    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
        GetComponent<SpriteRenderer>().enabled = false;
        MyTrigger = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (pulse > 0)
        {
            pulse -= 60 * Time.deltaTime;
            if (pulse <= 0)
            {
                MyTrigger.enabled = !MyTrigger.enabled;
                pulse = Random.Range(20,40);
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            sub = collision.transform.GetComponent<HomingAI>();

            if (sub.transform.position.x < GameInfo.PlayerPos.x) //if player is to RIGHT
            {
                if (sub.transform.position.x < transform.position.x)//if enemy AI  is LEFT of center of this guide, need to move up or down
                {
                    if (sub.transform.position.y < transform.position.y)
                    {
                        forceY = -30f;
                        forceX = 30;
                    }
                    else
                    {
                        forceY = 30f;
                        forceX = 30;
                    }
                }
            }
            if (sub.transform.position.x > GameInfo.PlayerPos.x) //if player is to LEFT
            {
                if (sub.transform.position.x > transform.position.x)//if enemy AI is RIGHT of center of this guide, need to move up or down
                {
                    if (sub.transform.position.y < transform.position.y)
                    {
                        forceY = -30f;
                        forceX = -30;
                    }
                    else
                    {
                        forceY = 30f;
                        forceX = -30;
                    }
                }
            }
            if (sub.transform.position.y > GameInfo.PlayerPos.y) //if player is BELOW
            {
                if (sub.transform.position.y > transform.position.y)//if enemy AI is ABOVE of center of this guide, need to left or right
                {
                    if (sub.transform.position.x < transform.position.x)
                    {
                        forceY = -30f;
                        forceX = -30;
                    }
                    else
                    {
                        forceY = -30f;
                        forceX = 30;
                    }
                }
            }
            if (sub.transform.position.y < GameInfo.PlayerPos.y) //if player is ABOVE
            {
                if (sub.transform.position.y < transform.position.y)//if enemy AI is BELOW of center of this guide, need to left or right
                {
                    if (sub.transform.position.x < transform.position.x)
                    {
                        forceY = 30f;
                        forceX = -30;
                    }
                    else
                    {
                        forceY = 30f;
                        forceX = 30;
                    }
                }
            }

            sub.ForceVector(forceX, forceY);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.tag == "Enemy")
            collision.transform.GetComponent<HomingAI>().ForceVector(0, 0);
    }


}
