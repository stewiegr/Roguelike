using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatus : MonoBehaviour
{
    public Animator MyAnim;
    public HomingAI MyNav;
    // Start is called before the first frame update

    public int Life;
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

    private void Start()
    {
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
    }

    public void TakeDmg(int _dmg, float _knockback)
    {
        GameInfo.PlayAudio(DmgSfxIndex);
        Life -= _dmg;
        if (Life > 0)
        {
            MyNav.Knockback(_knockback);
            MyAnim.SetTrigger("Hurt");
        }
        else if (Alive)
        {
            Die();
        }

        if (GameInfo.PlayerStatus.Relics.Lifesteal)
        {
            if (Random.Range(0, 100) > 98)
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
        GM.currentKillsThisWave++;
        GM.LivingEnemies--;
        GetComponent<Collider2D>().enabled = false;

        DoCurrencyDrop();
        DoRelicDrop();

        GM.CurrentLevel.RemoveMe(this.gameObject);
        if(Random.Range(0,10) > 7)
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
        if(ChanceToDropRelic>0 || ChanceToDropRareRelic > 0 || ChanceToDropLegendaryRelic >  0)
        {
            int roll = Random.Range(0, 101);

            if(roll > 100 - ChanceToDropLegendaryRelic)
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
