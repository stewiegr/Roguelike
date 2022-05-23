using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicBonuses : MonoBehaviour
{
    public bool ShieldBonus;
    public bool PenetratingProjectile;
    public int ApplesHeld;
    public bool Lifeline;
    public bool FlamingFootprints;
    public bool TripleShot;



    public void DetermineCurrentBonuses(List<Item> _inv)
    {
        WipeBonusesForRecount();
        foreach(Item item in _inv)
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
                        case Item.RelicBonus.Lifeline:
                            Lifeline = true;
                            break;
                        case Item.RelicBonus.FlamingFootprints:
                            FlamingFootprints = true;
                            break;
                        case Item.RelicBonus.TripleShot:
                            TripleShot = true;
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
        Lifeline = false;
        TripleShot = false;
        FlamingFootprints=false;
    }
}


