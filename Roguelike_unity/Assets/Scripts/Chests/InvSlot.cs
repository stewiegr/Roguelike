using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSlot : MonoBehaviour
{
    public Item GameItem;
    public SpriteRenderer ItemRenderer;

    public void UpdateSlot(Item _item)
    {
        GameItem = _item;
        ItemRenderer.sprite = GameItem.ItemGFX;
    }

    public void ClearSlot()
    {
        GameItem = null;
        ItemRenderer.sprite = null;
    }
}
