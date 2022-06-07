using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSlot : MonoBehaviour
{
    public Item GameItem;
    public SpriteRenderer ItemRenderer;
    ItemManagement MyItemGameObject;
    Transform MyItemGFX;
    bool dragging = false;
    public Vector3 HomePos;
    public ChestInventory MyChest;
    public PlayerInventory MyInv;
    public int IndexInInv;

    public enum SlotType
    {
        General,
        Weapon,
        Tome,
        Trinket,
        Relic,
        Universal
    }
    public SlotType MyType;


    private void Start()
    {
        MyItemGFX = ItemRenderer.transform;
        HomePos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        MyItemGameObject = GetComponentInChildren<ItemManagement>();
    }

    private void Update()
    {

            ClickListener();
            if (dragging)
            {
                MyItemGFX.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -6);
                ItemRenderer.sortingOrder = 16;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                ItemRenderer.sortingOrder = 15;
                if (hit.collider != null)
                {
                    if (hit.collider.transform.tag == "ItemSlot")
                    {
                        if (hit.collider.transform == this.transform && !Input.GetMouseButton(0) && GameItem != null)
                        {
                            if (GameItem.ItemName != "Empty Slot")
                            {
                                GameInfo.ItemInfoWindow.SetActive(true);
                                GameInfo.ItemInfoWindow.transform.position = (Vector2)MyItemGFX.position - new Vector2(2, 2);
                                GameInfo.ItemInfoWindow.GetComponent<ItemDesc>().SetFields(GameItem);
                            }
                            else
                                GameInfo.ItemInfoWindow.GetComponent<ItemDesc>().EmptySlotInfo(MyType, IndexInInv);
                        }
                    }
                    else
                    {
                        GameInfo.ItemInfoWindow.SetActive(false);
                    }
                }
                else
                    GameInfo.ItemInfoWindow.SetActive(false);
            }
        
    }
    public void UpdateSlot(Item _item)
    {
        if (_item != null)
        {
            GameItem = _item;
            ItemRenderer.sprite = GameItem.ItemGFX;
        }
        else
            ClearSlot();
    }

    public void ClearSlot()
    {
        GameItem = GameInfo.GM.GetComponent<ItemDatabase>().Empty;
        ItemRenderer.sprite = null;
    }

    public void ReturnHome()
    {
        dragging = false;
        if (MyItemGameObject != null)
        {
            MyItemGameObject.CancelInteract();
            MyItemGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
    }

    void ClickListener()
    {
        if (GameItem != null)
        {
            if (Input.GetMouseButtonDown(0) && !dragging && GameItem.ItemName != "Empty Slot" && GameInfo.GM.GameSpeed==1)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.transform == this.transform)
                    {
                        dragging = true;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && dragging || GameInfo.GM.GameSpeed == 0)
            {
                dragging = false;
                InvSlot swap = MyItemGameObject.DropItem();
                SwapItem(MyItemGameObject.DropItem());
            }
            if(Input.GetMouseButtonDown(1) && !dragging && GameItem.ItemName!="Empty Slot" && GameInfo.GM.GameSpeed==1)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.transform == this.transform)
                    {
                        if (GameItem.RelicBagA)
                            MyInv.AddItem(GameInfo.ItemDB.RelicA[Random.Range(0, GameInfo.ItemDB.RelicA.Length)], IndexInInv);
                        if (GameItem.RelicBagB)
                            MyInv.AddItem(GameInfo.ItemDB.RelicB[Random.Range(0, GameInfo.ItemDB.RelicB.Length)], IndexInInv);
                        if (GameItem.RelicBagC)
                            MyInv.AddItem(GameInfo.ItemDB.RelicC[Random.Range(0, GameInfo.ItemDB.RelicC.Length)], IndexInInv);
                    }
                }

            }
            if (Input.GetMouseButtonDown(2) && !dragging && GameItem.ItemName != "Empty Slot" && GameInfo.GM.GameSpeed == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.transform == this.transform)
                    {
                        ClearSlot();
                    }
                }

            }
        }
    }

    void UpdateParentInv()
    {
        if (MyChest != null)
            MyChest.MyItems[IndexInInv] = GameItem;
        if (MyInv != null)
            MyInv.MyItems[IndexInInv] = GameItem;
    }

    void SwapItem(InvSlot _swapWith)
    {
        Item ItemPH = GameItem;
        InvSlot.SlotType DestinationItem;
        InvSlot.SlotType DestinationSlot = _swapWith.MyType;
        if (_swapWith.GameItem != null)
            DestinationItem = _swapWith.GameItem.ItemType;
        else
            DestinationItem = InvSlot.SlotType.General;


        if (_swapWith.transform.name != this.transform.name && ((DestinationSlot == InvSlot.SlotType.Universal || DestinationSlot==InvSlot.SlotType.General) || (DestinationSlot == GameItem.ItemType) || DestinationSlot==InvSlot.SlotType.General && GameItem.ItemType!=InvSlot.SlotType.Relic) && ((MyType==InvSlot.SlotType.Universal || MyType==InvSlot.SlotType.General) || DestinationItem == MyType ||/* (DestinationItem!=InvSlot.SlotType.Relic && MyType==InvSlot.SlotType.General) ||*/ DestinationItem==InvSlot.SlotType.Universal))
        {
            if (_swapWith.GameItem != null)
            {
                UpdateSlot(_swapWith.GameItem);
                if (ItemPH != null)
                    _swapWith.UpdateSlot(ItemPH);
                else
                    _swapWith.ClearSlot();
            }
            else
            {
                if (ItemPH != null)
                {
                    _swapWith.UpdateSlot(ItemPH);
                }
                else
                    _swapWith.ClearSlot();

                ClearSlot();
            }
            this.MyItemGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        this.MyItemGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);


        UpdateParentInv();
        _swapWith.UpdateParentInv();
        GameInfo.PlayerStatus.UpdatePlayerStats();
    }
}
//((DestinationSlot == InvSlot.SlotType.Universal || (DestinationSlot == InvSlot.SlotType.General && ItemPH.ItemType!=InvSlot.SlotType.Relic) || _swapWith.MyType == ItemPH.ItemType)) && ((MyType == InvSlot.SlotType.General && DestinationItem!=InvSlot.SlotType.Relic) || MyType == DestinationItem || DestinationItem == InvSlot.SlotType.General && DestinationItem !=InvSlot.SlotType.Relic || DestinationItem==InvSlot.SlotType.Universal))