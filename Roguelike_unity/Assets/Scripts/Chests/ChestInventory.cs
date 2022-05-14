using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{

    public List<Item> MyItems;
    public int MaxItemCountUpTo9;
    
    // Start is called before the first frame update
    void Start()
    {
        MaxItemCountUpTo9 = Random.Range(2, MaxItemCountUpTo9);

        for(int i = 0; i <=MaxItemCountUpTo9; i++)
        {
            MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Common.Length)]);
        }
    }

 
}
