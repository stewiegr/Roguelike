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
            MoveSpeed.text = "Move Speed: " + _item.MoveSpeed.ToString();
            ItemDMG.text = "Damage: " + _item.Damage.ToString();
            ItemShotSpeed.text = "Shot Speed: " + _item.RateOfFire.ToString();
            ItemRange.text = "Range: " + _item.Range.ToString();
            ItemEnergy.text = "Energy: " + _item.EnergyRegen.ToString();
            ItemLuck.text = "Luck: " + _item.Luck.ToString();
            ItemLore.text = _item.ItemDescription;
            ItemSprite.sprite = _item.ItemGFX;
        }
        else
            gameObject.SetActive(false);
    }

}
