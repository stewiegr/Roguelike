using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShadow : MonoBehaviour
{

    public GameObject Shadow;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Shadow, (Vector2)transform.position + Vector2.down * .5f, transform.rotation);
    }

}
