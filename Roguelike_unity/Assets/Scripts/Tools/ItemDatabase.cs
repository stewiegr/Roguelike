using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Item[] Common;
    public Item[] Rare;
    public Item[] Unique;
    public Item[] Legendary;
    public Item[] RelicA;
    public Item[] RelicB;
    public Item[] RelicC;
    public Item Empty;

    public GameObject Coin;
    public GameObject CoinStack;
    public GameObject CoinBag;

    public GameObject RelicBagA;
    public GameObject RelicBagB;
    public GameObject RelicBagC;
    

    private void Awake()
    {
        GameInfo.ItemDB = this;
        Common = Resources.LoadAll<Item>("Items/Common");
        Rare = Resources.LoadAll<Item>("Items/Rare");
        Unique = Resources.LoadAll<Item>("Items/Unique");
        Legendary = Resources.LoadAll<Item>("Items/Legendary");
        RelicA = Resources.LoadAll<Item>("Items/RelicA");
        RelicB = Resources.LoadAll<Item>("Items/RelicB");
        RelicC = Resources.LoadAll<Item>("Items/RelicC");
    }
}
