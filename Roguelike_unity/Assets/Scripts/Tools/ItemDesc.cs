using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDesc : MonoBehaviour
{
    public TextMeshPro ItemName;
    public TextMeshPro MoveSpeed;
    public TextMeshPro ItemDMG;
    public TextMeshPro ItemShotSpeed;
    public TextMeshPro ItemRange;
    public TextMeshPro ItemEnergy;
    public TextMeshPro ItemLuck;
    public TextMeshPro ItemLore;
    public TextMeshPro RelicDesc;
    public SpriteRenderer ItemSprite;


    private void Start()
    {
        GameInfo.ItemInfoWindow = this.gameObject;
        gameObject.SetActive(false);
    }
    public void SetFields(Item _item)
    {
        if (_item != null)
        {
            ItemName.text = _item.ItemName;
            ItemLore.text = _item.ItemDescription;
            if (_item.ItemType != InvSlot.SlotType.Relic)
            {
                MoveSpeed.text = "Move Speed: " + _item.MoveSpeed.ToString();
                ItemDMG.text = "Damage: " + _item.Damage.ToString();
                ItemShotSpeed.text = "Shot Speed: " + _item.RateOfFire.ToString();
                ItemRange.text = "Range: " + _item.Range.ToString();
                ItemEnergy.text = "Energy: " + _item.EnergyRegen.ToString();
                ItemLuck.text = "Luck: " + _item.Luck.ToString();
                RelicDesc.text = "";
            }
            else
            {
                MoveSpeed.text = "";
                ItemDMG.text = "";
                ItemShotSpeed.text = "";
                ItemRange.text = "";
                ItemEnergy.text = "";
                ItemLuck.text = "";
                RelicDesc.text = _item.RelicDesc;
            }

            ItemSprite.sprite = _item.ItemGFX;
        }
        else
            gameObject.SetActive(false);
    }

    public void EmptySlotInfo(InvSlot.SlotType _type, int _ind)
    {
        ItemSprite.sprite = null;
        if (_ind < 16)
        {
            
            if (_type == InvSlot.SlotType.Relic)
            {
                ItemName.text = "EMPTY RELIC SLOT";
                MoveSpeed.text = "";
                ItemDMG.text = "";
                ItemShotSpeed.text = "";
                ItemRange.text = "";
                ItemEnergy.text = "";
                ItemLuck.text = "";
                RelicDesc.text = "Place a RELIC ITEM in this slot to activate its effects.";
                ItemLore.text = "";
                
            }
            else
            {
                ItemName.text = "EMPTY NORMAL SLOT";
                MoveSpeed.text = "";
                ItemDMG.text = "";
                ItemShotSpeed.text = "";
                ItemRange.text = "";
                ItemEnergy.text = "";
                ItemLuck.text = "";
                RelicDesc.text = "Store CONSUMABLES or EQUIPMENT ITEMS in this slot.";
                ItemLore.text = "";
            }
        }
        else
        {
            if(_ind==16)
            {
                ItemName.text = "STAFF SLOT";
                MoveSpeed.text = "";
                ItemDMG.text = "";
                ItemShotSpeed.text = "";
                ItemRange.text = "";
                ItemEnergy.text = "";
                ItemLuck.text = "";
                RelicDesc.text = "Put a STAFF in this slot to change your attack";
                ItemLore.text = "pew pew";
            }
            if (_ind == 17)
            {
                ItemName.text = "TOME SLOT";
                MoveSpeed.text = "";
                ItemDMG.text = "";
                ItemShotSpeed.text = "";
                ItemRange.text = "";
                ItemEnergy.text = "";
                ItemLuck.text = "";
                RelicDesc.text = "Put a TOME in this slot to change your active spell";
                ItemLore.text = "ew... bookworms.";
            }
            if (_ind >17)
            {
                ItemName.text = "TRINKET SLOT";
                MoveSpeed.text = "";
                ItemDMG.text = "";
                ItemShotSpeed.text = "";
                ItemRange.text = "";
                ItemEnergy.text = "";
                ItemLuck.text = "";
                RelicDesc.text = "Put a TRINKET in this slot to gain its bonus stats";
                ItemLore.text = "";
            }
        }
    }

}
