using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDB : MonoBehaviour
{
    public GameObject BossHB;
    public Transform BossHBPos;
    public GameObject RelicBagTransition;
    public TextMeshProUGUI Gold;
    public float goldUICounter = 0;
    public float goldAmt = 0;

    private void Awake()
    {
        GameInfo.UIDB = this;
    }
    private void Update()
    {
        if (goldUICounter > 0)
            goldUICounter -= 60 * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (goldUICounter <= 0 && Gold.text != "")
        {
            Gold.text = "";
            goldAmt = 0;
        }
        if(goldUICounter>0 && goldUICounter < 50 && Gold.fontSize!=16)
        {
            Gold.fontSize--;
        }

    }
}
