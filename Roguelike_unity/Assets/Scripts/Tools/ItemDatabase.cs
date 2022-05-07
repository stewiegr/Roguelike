using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Item[] Common;
    public Item[] Rare;
    public Item[] Unique;
    public Item[] Legendary;

    private void Awake()
    {
        GameInfo.ItemDB = this;
        Common = Resources.LoadAll<Item>("Items/Common");
        Rare = Resources.LoadAll<Item>("Items/Rare");
        Unique = Resources.LoadAll<Item>("Items/Unique");
        Legendary = Resources.LoadAll<Item>("Items/Legendary");
    }
}
