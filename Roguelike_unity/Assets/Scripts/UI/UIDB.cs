using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDB : MonoBehaviour
{
    public GameObject BossHB;
    public Transform BossHBPos;
    public GameObject RelicBagTransition;

    private void Awake()
    {
        GameInfo.UIDB = this;
    }
}
