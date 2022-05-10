using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> MyItems = new List<Item>();
    public List<InvSlot> Squares;
    public InvSlot Weapon;
    public InvSlot Tome;
    public InvSlot Trinket1;
    public InvSlot Trinket2;

    private void Start()
    {
        for(int i=0; i<=15; i++)
        {
            MyItems.Add(null);
            Squares[i].IndexInInv = i;
        }
    }

    public void CloseInv()
    {
        for (int i = 0; i <= 15; i++)
        {
            Squares[i].GetComponent<InvSlot>().ReturnHome();
        }
    }

    public float CalcDmg()
    {
        float z = 0;
        if (Weapon.GameItem != null)
            z += Weapon.GameItem.Damage;
        if (Tome.GameItem != null)
            z += Tome.GameItem.Damage;
        if (Trinket1.GameItem != null)
            z += Trinket1.GameItem.Damage;
        if (Trinket2.GameItem != null)
            z += Trinket2.GameItem.Damage;

        return z;
    }
    public float CalcSpeed()
    {
        float z = 0;
        if (Weapon.GameItem != null)
            z += Weapon.GameItem.MoveSpeed;
        if (Tome.GameItem != null)
            z += Tome.GameItem.MoveSpeed;
        if (Trinket1.GameItem != null)
            z += Trinket1.GameItem.MoveSpeed;
        if (Trinket2.GameItem != null)
            z += Trinket2.GameItem.MoveSpeed;

        return z;
    }

    public float CalcRange()
    {
        float z = 0;
        if (Weapon.GameItem != null)
            z += Weapon.GameItem.Range;
        if (Tome.GameItem != null)
            z += Tome.GameItem.Range;
        if (Trinket1.GameItem != null)
            z += Trinket1.GameItem.Range;
        if (Trinket2.GameItem != null)
            z += Trinket2.GameItem.Range;

        return z;
    }

    public float CalcROF()
    {
        float z = 0;
        if (Weapon.GameItem != null)
            z += Weapon.GameItem.RateOfFire;
        if (Tome.GameItem != null)
            z += Tome.GameItem.RateOfFire;
        if (Trinket1.GameItem != null)
            z += Trinket1.GameItem.RateOfFire;
        if (Trinket2.GameItem != null)
            z += Trinket2.GameItem.RateOfFire;

        return z;
    }

    public float CalcLuck()
    {
        float z = 0;
        if (Weapon.GameItem != null)
            z += Weapon.GameItem.Luck;
        if (Tome.GameItem != null)
            z += Tome.GameItem.Luck;
        if (Trinket1.GameItem != null)
            z += Trinket1.GameItem.Luck;
        if (Trinket2.GameItem != null)
            z += Trinket2.GameItem.Luck;

        return z;
    }

}
