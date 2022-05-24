using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;

    public float MoveSpeed;
    public float RateOfFire;
    public float EnergyRegen;
    public float Damage;
    public float Range;
    public float Luck;

    public Sprite ItemGFX;
    public string RelicDesc;

    public GameObject StaffProjectile;

    public InvSlot.SlotType ItemType;


    public enum RelicBonus
    {
        None,
        AppleEffect,
        ShieldBonus,
        PenetratingProjectile,
        TripleShot,
        TrackingShots,
        LifeSteal,
        Lifeline,
        Forcefield,
        TeleportExplosion,
        FlamingFootprints,
        KnockbackBonus
    }

    public RelicBonus RelicEffect;

}
