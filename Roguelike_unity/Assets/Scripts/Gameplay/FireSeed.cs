using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSeed : MonoBehaviour
{
    bool landed = false;
    public Vector2 initialVector;
    public GameObject Fire;
    public int Dmg = 2;

    private void Start()
    {
        initialVector = new Vector2(Random.Range(-8, 8), Random.Range(15, 22));      
    }

    // Update is called once per frame
    void Update()
    {
        if (!landed)
        {
            if (initialVector.y > -12)
            {
                transform.position += Vector3.ClampMagnitude((Vector3)initialVector, 25) * Time.deltaTime;
                initialVector.y -= .9f * 60 * Time.deltaTime;
            }
            else
            {
                landed = true;
            }
        }
        if(landed)
        {
            Instantiate(Fire, transform.position, transform.rotation);
            GameObject.Destroy(this.gameObject);
        }
    }


   
}
