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
    public bool Lifesteal;
    public bool TrackingShots;
    public int AttackFairyCount;
    public List<GameObject> AttackFairy;


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
                        case Item.RelicBonus.LifeSteal:
                            Lifesteal = true;
                            break;
                        case Item.RelicBonus.TrackingShots:
                            TrackingShots = true;
                            break;
                        case Item.RelicBonus.AttackFairy:
                            AttackFairyCount++;
                            break;

                    }
                }
            }
        }
        GameInfo.PlayerStatus.DoApples(ApplesHeld);
        DoAttackFairies();
            
    }

    void WipeBonusesForRecount()
    {
        AttackFairyCount = 0;
        ShieldBonus = false;
        PenetratingProjectile = false;
        ApplesHeld = 0;
        Lifeline = false;
        TripleShot = false;
        FlamingFootprints=false;
        Lifesteal = false;
        TrackingShots = false;
    }

    void DoAttackFairies()
    {
        if(AttackFairy.Count < AttackFairyCount)
        {
            GameObject Atk = (Instantiate(GameInfo.EffectsDB.AttackFairy, transform.position, transform.rotation));
            AttackFairy.Add(Atk);
            Atk.GetComponent<AttackFairy>().Player = GameInfo.Player;
        }
        if(AttackFairy.Count > AttackFairyCount)
        {
            GameObject.Destroy(AttackFairy[AttackFairyCount]);
            AttackFairy.RemoveAt(AttackFairyCount);
        }
    }
}


