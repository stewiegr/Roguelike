using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvID : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Pos;
    void Awake()
    {
        GameInfo.Inv = this.transform;
    }

    private void Update()
    {
        transform.position = (Vector2)Pos.position + Vector2.right * 2;
    }

}
