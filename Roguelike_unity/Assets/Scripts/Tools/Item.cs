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

    public enum ItemSlotType
    {Weapon, Tome, Trinket}

    public ItemSlotType ItemType;

}