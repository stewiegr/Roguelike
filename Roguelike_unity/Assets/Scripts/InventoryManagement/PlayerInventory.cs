using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> MyItems = new List<Item>();
    public List<InvSlot> Squares;
    public List<InvSlot> AllSquares;
    public InvSlot Weapon;
    public InvSlot Tome;
    public InvSlot Trinket1;
    public InvSlot Trinket2;

    private void Awake()
    {
        for(int i=0; i<=19; i++)
        {
            MyItems.Add(null);
            if (i <= 15)
                Squares[i].IndexInInv = i;
            else if (i == 16) Weapon.IndexInInv = i;
            else if (i == 17) Tome.IndexInInv = i;
            else if (i == 18) Trinket1.IndexInInv = i;
            else if (i == 19) Trinket2.IndexInInv = i;
        }
    }

    public void CloseInv()
    {
        for (int i = 0; i <= 15; i++)
        {
            Squares[i].GetComponent<InvSlot>().ReturnHome();
        }
    }

    public void FindAndRemove(Item.RelicBonus _relic)
    {
        for(int i=8; i<=15; i++)
        {
            if (Squares[i].GameItem.RelicEffect == _relic)
            {
                Squares[i].ClearSlot();
                MyItems[i] = null;
                break;
            }
        }
    }

    public int EmptySlotAvail()
    {
        for(int i=0; i<=7; i++)
        {
            if(MyItems[i]==GameInfo.ItemDB.Empty || MyItems[i]==null)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddItem(Item _item, int _slot)
    {
        MyItems[_slot] = _item;
        if (Squares[_slot].updateDel <= 0)
            Squares[_slot].UpdateSlot(_item);
        else
            Squares[_slot].UpdateSlotWithDelay(_item);
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
