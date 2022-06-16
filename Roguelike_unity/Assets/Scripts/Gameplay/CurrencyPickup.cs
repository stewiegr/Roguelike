using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    bool readyForPickup = false;
    bool inPosition = false;
    bool pickup = false;
    Vector2 initialVector;
    float settleTime = 30;

    public int value = 0;
    public Item ItemToAdd = null;
    public int relicLevel = -1;

    private void Start()
    {
        initialVector = new Vector2(Random.Range(-4, 4), Random.Range(10, 15));
        if(relicLevel!=-1)
        AnimateMe();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyForPickup && !inPosition)
        {
            if (initialVector.y > -12)
            {
                transform.position += Vector3.ClampMagnitude((Vector3)initialVector, 25) * Time.deltaTime;
                initialVector.y -= .9f * 60 * Time.deltaTime;
            }
            else
            {
                inPosition = true;
            }
        }
        if(inPosition && !readyForPickup)
        {
            settleTime -= 60 * Time.deltaTime;
            if (settleTime <= 0)
                readyForPickup = true;
        }
        if(pickup)
        {
            if (readyForPickup)
            {
                if (Vector2.Distance(transform.position, GameInfo.PlayerPos + Vector2.up * .5f) > .5f)
                    transform.position = Vector2.MoveTowards(transform.position, GameInfo.PlayerPos + Vector2.up * .5f, 20 * Time.deltaTime);
                else
                {
                    GameInfo.PlayAudio(1);
                    if (value != 0)
                    {
                        GameInfo.GM.AddGold(value);
                        GameObject.Destroy(this.gameObject);
                    }
                    if (ItemToAdd != null)
                    {
                        int slot = GameInfo.Player.GetComponent<PlayerInventory>().EmptySlotAvail();
                        if (slot != -1)
                        {
                            GameInfo.Player.GetComponent<PlayerInventory>().AddItem(ItemToAdd, slot);
                            GameObject.Destroy(this.gameObject);
                        }
                        else
                        {
                            pickup = false;
                            readyForPickup = false;
                            inPosition = false;
                        }
                    }

                }
            }
        }
    }

    public void PickedUp()
    {
       
        pickup = true;
    }

    public void AnimateMe()
    {
        switch(relicLevel)
        {
            case 0:
                GetComponentInChildren<Animator>().SetBool("Rare", true);
                break;
            case 1:
                GetComponentInChildren<Animator>().SetBool("Unique", true);
                break;
            case 2:
                GetComponentInChildren<Animator>().SetBool("Legend", true);
                break;

        }
    }
}
