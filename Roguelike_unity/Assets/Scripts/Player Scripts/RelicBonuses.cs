using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicBonuses : MonoBehaviour
{
    public bool ShieldBonus;
    public bool PenetratingProjectile;
    public int ApplesHeld;



    public void DetermineCurrentBonuses(PlayerInventory _inv)
    {
        WipeBonusesForRecount();
        foreach(Item item in _inv.MyItems)
        {
            if (item != null)
            {
                if (item.ItemType == InvSlot.SlotType.Relic)
                {
                    switch (item.RelicEffect)
                    {
                        case Item.RelicBonus.AppleEffect:
                            if(ApplesHeld<3)
                            ApplesHeld++;
                            break;
                        case Item.RelicBonus.PenetratingProjectile:
                            PenetratingProjectile = true;
                            break;
                        case Item.RelicBonus.ShieldBonus:
                            ShieldBonus = true;
                            break;
                    }
                }
            }
        }
        GameInfo.PlayerStatus.DoApples(ApplesHeld);
    }

    void WipeBonusesForRecount()
    {
        ShieldBonus = false;
        PenetratingProjectile = false;
        ApplesHeld = 0;
    }
}


