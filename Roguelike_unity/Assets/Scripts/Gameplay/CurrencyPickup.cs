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

    public int value;

    private void Start()
    {
        initialVector = new Vector2(Random.Range(-4, 4), Random.Range(10, 15));
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
      /*  if(readyForPickup && !pickup)
        {
            if(Vector2.Distance(transform.position, GameInfo.PlayerPos) < 3)
            {
                pickup = true;             
            }
        }*/
        if(pickup)
        {
            if (Vector2.Distance(transform.position, GameInfo.PlayerPos) > .5f)
                transform.position = Vector2.MoveTowards(transform.position, GameInfo.PlayerPos, 20 * Time.deltaTime);
            else
            {
                GameObject.Destroy(this.gameObject);
                GameInfo.GM.AddGold(value);
            }
        }
    }

    public void PickedUp()
    {
        pickup = true;
    }
}
