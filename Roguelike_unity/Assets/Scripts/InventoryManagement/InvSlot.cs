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
    Vector3 HomePos;
    public ChestInventory MyChest;
    //public PlayerInv MyInv;
    public int IndexInInv;


    private void Start()
    {
        MyItemGFX = ItemRenderer.transform;
        HomePos = MyItemGFX.transform.position;
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
                Debug.Log(hit.transform.name);
                if (hit.collider.transform == this.transform)
                {
                    GameInfo.ItemInfoWindow.SetActive(true);
                    GameInfo.ItemInfoWindow.GetComponent<ItemDesc>().SetFields(GameItem);
                }
            }
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
        MyItemGameObject.transform.position = HomePos;
    }

    void ClickListener()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);
                if (hit.collider.transform == this.transform)
                {
                    dragging = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
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
    }

    void SwapItem(InvSlot _swapWith)
    {
        Item ItemPH = GameItem;
        if (_swapWith.transform.name != this.transform.name)
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
            MyItemGameObject.transform.position = HomePos;
        }
        MyItemGameObject.transform.position = HomePos;


        UpdateParentInv();
        _swapWith.UpdateParentInv();
            
    }
}
