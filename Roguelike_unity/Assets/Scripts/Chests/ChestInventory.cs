using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : MonoBehaviour
{

    public List<Item> MyItems;
    public int MaxItemCountUpTo9;
    public float ChanceOfRare=0;
    public float ChanceOfUnique=0;
    public float ChanceOfLegendary=0;

    int staffCt = 0;
    int trinketCt = 0;
    int tomeCt = 0;

    // Start is called before the first frame update
    void Start()
    {
        MaxItemCountUpTo9 = Random.Range(2, MaxItemCountUpTo9);

        for (int i = 0; i < 9; i++)
        {
            float loot = Random.Range(0f, 100f);
            if (i <= MaxItemCountUpTo9)
            {

                if (loot > 100 - ChanceOfLegendary)
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Legendary.Length)]);
                else if (loot > 100 - ChanceOfUnique)
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Unique.Length)]);
                else if (loot > 100 - ChanceOfRare)
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Rare.Length)]);
                else
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Common.Length)]);
            }
            else
            {
                MyItems.Add(GameInfo.ItemDB.Empty);
            }
        }
    }

    public void ShuffleLoot()
    {
        MyItems.Clear();
        for (int i = 0; i < 9; i++)
        {
            float loot = Random.Range(0f, 100f);
            if (i <= 9)
            {
                if (loot > 100 - ChanceOfLegendary)
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Legendary.Length)]);
                else if (loot > 100 - ChanceOfUnique)
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Unique.Length)]);
                else if (loot > 100 - ChanceOfRare)
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Rare.Length)]);
                else
                    MyItems.Add(GameInfo.ItemDB.Common[Random.Range(0, GameInfo.ItemDB.Common.Length)]);
            }
            else
            {
                MyItems.Add(GameInfo.ItemDB.Empty);
            }
        }
    }
}
