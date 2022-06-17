using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicBonuses : MonoBehaviour
{
    public int ShieldBonus;
    public int PenetratingProjectile;
    public int ApplesHeld;
    public int Lifeline;
    public int FlamingFootprints;
    public int TripleShot;
    public int Lifesteal;
    public int TrackingShots;
    public int AttackFairyCount;
    public int Forcefield;
    public List<GameObject> AttackFairy;
    public GameObject FField;


    public void DetermineCurrentBonuses(List<Item> _inv)
    {
        WipeBonusesForRecount();
        for(int i=8; i<=15; i++)
        {
            if (_inv[i] != null)
            {
                if (_inv[i].ItemType == InvSlot.SlotType.Relic)
                {
                    switch (_inv[i].RelicEffect)
                    {
                        case Item.RelicBonus.AppleEffect:
                            if(ApplesHeld<3)
                            ApplesHeld++;
                            break;
                        case Item.RelicBonus.PenetratingProjectile:
                            PenetratingProjectile++;
                            break;
                        case Item.RelicBonus.ShieldBonus:
                            ShieldBonus++;
                            break;
                        case Item.RelicBonus.Lifeline:
                            Lifeline++;
                            break;
                        case Item.RelicBonus.FlamingFootprints:
                            FlamingFootprints++;
                            break;
                        case Item.RelicBonus.TripleShot:
                            TripleShot++;
                            break;
                        case Item.RelicBonus.LifeSteal:
                            Lifesteal++;
                            break;
                        case Item.RelicBonus.TrackingShots:
                            TrackingShots++;
                            break;
                        case Item.RelicBonus.AttackFairy:
                            AttackFairyCount++;
                            break;
                        case Item.RelicBonus.Forcefield:
                            Forcefield++;
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
        ShieldBonus = 0;
        PenetratingProjectile = 0;
        ApplesHeld = 0;
        Lifeline = 0;
        TripleShot = 0;
        FlamingFootprints=0;
        Lifesteal = 0;
        TrackingShots = 0;
        Forcefield = 0;
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

    public int SetProjectilePenetrations()
    {
        return Random.Range(3 + PenetratingProjectile, 5 + PenetratingProjectile);
    }

    public int CheckShieldBonus()
    {
        int bonus = Random.Range(0, 100 + ShieldBonus * 3);
        return bonus;
    }

    public int CalculateForcefieldDmg()
    {
        return 3;
    }

    public int CheckLifestealChance()
    {
        return Random.Range(0, 100 + Lifesteal * 5);
    }
}


