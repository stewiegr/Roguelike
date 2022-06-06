using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyVac : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Currency")
        {
            collision.transform.GetComponent<CurrencyPickup>().PickedUp();
        }
    }
}
