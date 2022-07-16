using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatus : MonoBehaviour
{
    public Animator MyAnim;
    public HomingAI MyNav;
    // Start is called before the first frame update

    public bool IgnoreGMCounts = false;
    public bool NoKnockback;
    public int Life;
    public int maxLife;
    public int AtkDmg;
    //public float AtkRange;
    public float AtkDly = 60;
    public float preventAtk = 0;
    public bool Alive = true;
    public List<GameObject> Currency;
    public float RunSpeed;
    public GameManager GM;

    public int DmgSfxIndex;
    public int DeadSfxIndex;

    public Vector2 CurrencyDropLowerUpper;
    int CurrencyActual;
    public float ChanceToDropRelic;
    public float ChanceToDropRareRelic;
    public float ChanceToDropLegendaryRelic;
    public BossDamageFlash DmgFlashEffect;
    public BossHealthbar BossHealthBar;


    public bool Shielded = false;

    public bool RequireBigBossHealthbar;
    public string BossName;

    private void Start()
    {
        maxLife = Life;
        GameObject PHCur;
        int spawnedCur = 0;
        CurrencyActual = (int)Random.Range(CurrencyDropLowerUpper.x, CurrencyDropLowerUpper.y);
        if (CurrencyActual > 0)
        {
            do
            {
                if (CurrencyActual - spawnedCur >= 10)
                {
                    PHCur = (Instantiate(GameInfo.ItemDB.CoinBag));
                    Currency.Add(PHCur);
                    PHCur.SetActive(false);
                    spawnedCur += 10;
                }
                else if (CurrencyActual - spawnedCur >= 5)
                {
                    PHCur = (Instantiate(GameInfo.ItemDB.CoinStack));
                    Currency.Add(PHCur);
                    PHCur.SetActive(false);
                    spawnedCur += 5;
                }
                else if (CurrencyActual - spawnedCur >= 1)
                {
                    PHCur = (Instantiate(GameInfo.ItemDB.Coin));
                    Currency.Add(PHCur);
                    PHCur.SetActive(false);
                    spawnedCur += 1;
                }
            }
            while (spawnedCur < CurrencyActual);
        }

        /*if(Gibs.Count>0)
        {
            for(int i =0; i<Gibs.Count; i++)
            {
                Gibs[i] = Instantiate(Gibs[i]);
                Gibs[i].SetActive(false);
                GM.TemporaryDebris.Add(Gibs[i]);
            }
        }*/

        if (Random.Range(0, 30) > 28)
            RunSpeed = 6;

        if (RequireBigBossHealthbar)
        {
            MainBossHealthBar MyHealthbar = Instantiate(GameInfo.UIDB.BossHB, GameInfo.UIDB.BossHBPos.position, transform.rotation, GameInfo.UIDB.transform).GetComponent<MainBossHealthBar>();
            MyHealthbar.InitHealthBar(this, BossName);
        }
    }

    public void Heal(int _healAmt)
    {
        Life += _healAmt;
        if (Life > maxLife)
            Life = maxLife;

        if (DmgFlashEffect != null)
            DmgFlashEffect.FlashHeal();
    }

    public void TakeDmg(int _dmg, float _knockback)
    {
        bool crit = false;
        GameInfo.PlayAudio(DmgSfxIndex);

        if (DmgFlashEffect != null)
            DmgFlashEffect.FlashDmg();

        if (Random.Range(0, 100) < GameInfo.PlayerStatus.Luck)
        {
            crit = true;
            _dmg *= 2;
        }

        _dmg += Mathf.RoundToInt(Random.Range((float)-_dmg * .2f, (float)_dmg * .2f));

        PopupText popup = Instantiate(GameInfo.EffectsDB.CustomText, (Vector2)transform.position + Vector2.up, transform.rotation).GetComponent<PopupText>();
        if (!crit)
            popup.InitTextCustom(_dmg.ToString(), 4, Color.red, 8);
        else
        {
            popup.InitTextCustom(_dmg.ToString(), 4, Color.red, 12);
            CamID.CMController.ShakeScreen(3, 1);
        }

        Life -= _dmg;
        if (BossHealthBar != null)
        {
            BossHealthBar.SetHealthBar(Life, maxLife);
        }

        if (Life > 0)
        {
            MyAnim.SetTrigger("Hurt");
            if (!NoKnockback && MyNav != null)
                MyNav.Knockback(_knockback);

        }
        else if (Alive)
        {
            if (MyNav != null)
                MyNav.StopMovement();
            Die();
        }

        if (GameInfo.PlayerStatus.Relics.Lifesteal > 0)
        {
            if (GameInfo.PlayerStatus.Relics.CheckLifestealChance() > 98)
            {
                GameInfo.PlayerStatus.LifelineImg.SetActive(true);
                GameInfo.PlayerStatus.LifelineImg.transform.position = GameInfo.PlayerPos;
                GameInfo.PlayerStatus.LifelineImg.GetComponent<SpriteRenderer>().sprite = GameInfo.PlayerStatus.ScytheSpr;
                GameInfo.PlayerStatus.HealPlayer(1);
            }
        }

    }

    void Die()
    {
        GameInfo.PlayAudio(DeadSfxIndex);
        Alive = false;
        if (!IgnoreGMCounts)
        {
            if (GM == null)
                GM = GameInfo.GM;
            GM.currentKillsThisWave++;
            GM.LivingEnemies--;
            GM.CurrentLevel.RemoveMe(this.gameObject);
        }
        GetComponent<Collider2D>().enabled = false;
        DoCurrencyDrop();
        DoRelicDrop();


        if (Random.Range(0, 10) > 7)
            CamID.CMController.ShakeScreen(1, 4);
        MyAnim.SetTrigger("Die");


    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void DoCurrencyDrop()
    {
        if (Currency.Count > 0)
        {
            for (int i = 0; i < Currency.Count; i++)
            {
                Currency[i].transform.position = this.transform.position;
                Currency[i].SetActive(true);
            }
        }
    }

    void DoRelicDrop()
    {
        if (ChanceToDropRelic > 0 || ChanceToDropRareRelic > 0 || ChanceToDropLegendaryRelic > 0)
        {
            int roll = Random.Range(0, 101);

            if (roll > 100 - ChanceToDropLegendaryRelic)
            {
                Instantiate(GameInfo.ItemDB.RelicBagA, transform.position, transform.rotation);
            }
            else if (roll > 100 - ChanceToDropRareRelic)
            {
                Instantiate(GameInfo.ItemDB.RelicBagB, transform.position, transform.rotation);
            }
            else if (roll > 100 - ChanceToDropRelic)
            {
                Instantiate(GameInfo.ItemDB.RelicBagC, transform.position, transform.rotation);
            }

        }
    }
}
