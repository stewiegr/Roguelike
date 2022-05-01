using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavRedir : MonoBehaviour
{
    Vector2 Pos;
    HomingAI sub;
    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    { 
        if (collision.transform.tag == "Enemy")
        {
            sub = collision.transform.GetComponent<HomingAI>();
            
            if (sub.GiveMovementVector().x !=0)
            {
                if (sub.transform.position.y > transform.position.y)
                    sub.ForceVector(0, .5f);
                else
                    sub.ForceVector(0, -.5f);
            }


        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {       
        if (collision.transform.tag == "Enemy")
            collision.transform.GetComponent<HomingAI>().ForceVector(0, 0);
    }


}
