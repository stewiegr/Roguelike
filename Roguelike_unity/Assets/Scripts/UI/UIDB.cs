using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDB : MonoBehaviour
{
    public GameObject BossHB;
    public Transform BossHBPos;

    private void Awake()
    {
        GameInfo.UIDB = this;
    }
}