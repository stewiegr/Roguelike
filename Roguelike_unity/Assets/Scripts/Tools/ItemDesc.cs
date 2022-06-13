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

    public Color GreenCol;

    public List<TextMeshPro> TextFields;


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
            switch(_item.ItemType)
            {
                case InvSlot.SlotType.Weapon:
                    ItemLore.text = "EQUIPMENT: WEAPON";
                    break;
                case InvSlot.SlotType.Tome:
                    ItemLore.text = "EQUIPMENT: TOME";
                    break;
                case InvSlot.SlotType.Trinket:
                    ItemLore.text = "EQUIPMENT: TRINKET";
                    break;
                case InvSlot.SlotType.Relic:
                    ItemLore.text = "RELIC ITEM";
                    break;
                case InvSlot.SlotType.General:
                    ItemLore.text = "CONSUMABLE";
                    break;

            }
            if (_item.ItemType != InvSlot.SlotType.Relic && _item.ItemType!=InvSlot.SlotType.General)
            {
                foreach(TextMeshPro tx in TextFields)
                {
                    tx.text = "";
                }

                int index = 0;
                if(_item.MoveSpeed!=0)
                {
                    TextFields[index].text = "Move Speed: "+_item.MoveSpeed.ToString();
                    TextFields[index].color = ColorText((int)_item.MoveSpeed);
                    index++;
                }
                if(_item.Damage!=0)
                {
                    TextFields[index].text = "Damage: " + _item.Damage.ToString();
                    TextFields[index].color = ColorText((int)_item.Damage);
                    index++;
                }
                if(_item.RateOfFire!=0)
                {
                    TextFields[index].text = "Shot Speed: " + _item.RateOfFire.ToString();
                    TextFields[index].color = ColorText((int)_item.RateOfFire);
                    index++;
                }
                if(_item.Range!=0)
                {
                    TextFields[index].text = "Range: " + _item.Range.ToString();
                    TextFields[index].color = ColorText((int)_item.Range);
                    index++;
                }
                if(_item.EnergyRegen!=0)
                {
                    TextFields[index].text = "Energy: " + _item.EnergyRegen.ToString();
                    TextFields[index].color = ColorText((int)_item.EnergyRegen);
                    index++;
                }
                if(_item.Luck!=0)
                {
                    TextFields[index].text = "Luck: " + _item.Luck.ToString();
                    TextFields[index].color = ColorText((int)_item.Luck);
                }
                /*
                MoveSpeed.text = "Move Speed: " + _item.MoveSpeed.ToString();
                MoveSpeed.color = ColorText((int)_item.MoveSpeed);
                ItemDMG.text = "Damage: " + _item.Damage.ToString();
                ItemDMG.color = ColorText((int)_item.Damage);
                ItemShotSpeed.text = "Shot Speed: " + _item.RateOfFire.ToString();
                ItemShotSpeed.color = ColorText((int)_item.RateOfFire);
                ItemRange.text = "Range: " + _item.Range.ToString();
                ItemRange.color = ColorText((int)_item.Range);
                ItemEnergy.text = "Energy: " + _item.EnergyRegen.ToString();
                ItemEnergy.color = ColorText((int)_item.EnergyRegen);
                ItemLuck.text = "Luck: " + _item.Luck.ToString();
                ItemLuck.color = ColorText((int)_item.Luck);
                */
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

    Color ColorText(int value)
    {
        if (value < 0)
            return Color.red;
        if (value > 0)
            return GreenCol;
        if (value == 0)
            return Color.black;
        else
            return Color.black;

    }

}
