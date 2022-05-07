using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{

    public List<Item> MyItems;
    int ItemCount;
    
    // Start is called before the first frame update
    void Start()
    {
        ItemCount = Random.Range(0, 6);

        for(int i = 0; i <=ItemCount; i++)
        {
            MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Common.Length)]);
        }
    }

 
}
