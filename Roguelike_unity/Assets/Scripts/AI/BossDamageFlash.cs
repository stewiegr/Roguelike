using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageFlash : MonoBehaviour
{
    public Material DmgFlashMat;
    public Material DefaultMat;
    SpriteRenderer MyRend;
    float flashDur = 0;
    // Update is called once per frame


    private void Awake()
    {
        GetComponent<NPCStatus>().DmgFlashEffect = this;
        MyRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (flashDur > 0)
        {
            flashDur -= 60 * Time.deltaTime;
            if (flashDur <= 0)
            {
                MyRend.material = DefaultMat;
                MyRend.color = Color.white;
            }
        }
    }

    public void FlashDmg()
    {
        flashDur = 5;
        MyRend.material = DmgFlashMat;
        MyRend.color = Color.red;
    }

    public void FlashHeal()
    {
        flashDur = 5;
        MyRend.material = DmgFlashMat;
        MyRend.color = Color.blue;
    }
}
