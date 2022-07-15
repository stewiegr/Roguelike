using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsDB : MonoBehaviour
{
    public GameObject SmallFlame;
    public GameObject AttackFairy;
    public GameObject CustomText;
    public GameObject Scorch;
    public GameObject Bomb;
    private void Start()
    {
        GameInfo.EffectsDB = this;
    }
}
