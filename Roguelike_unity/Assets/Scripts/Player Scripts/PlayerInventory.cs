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


}
