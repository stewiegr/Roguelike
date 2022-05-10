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
        Trinket
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
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.transform.tag == "ItemSlot")
                {
                    if (hit.collider.transform == this.transform)
                    {
                        GameInfo.ItemInfoWindow.SetActive(true);
                        GameInfo.ItemInfoWindow.GetComponent<ItemDesc>().SetFields(GameItem);
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
        GameItem = null;
        ItemRenderer.sprite = null;
    }

    public void ReturnHome()
    {
        dragging = false;
        MyItemGameObject.CancelInteract();
        MyItemGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
    }

    void ClickListener()
    {
        if (Input.GetMouseButtonDown(0) && !dragging)
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
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            InvSlot swap = MyItemGameObject.DropItem();
            SwapItem(MyItemGameObject.DropItem());
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


        if (_swapWith.transform.name != this.transform.name && ((DestinationSlot == InvSlot.SlotType.General || _swapWith.MyType == ItemPH.ItemType)) && (MyType==InvSlot.SlotType.General || MyType==DestinationItem || DestinationItem == InvSlot.SlotType.General))
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
