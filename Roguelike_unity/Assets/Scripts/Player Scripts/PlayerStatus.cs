using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxLife = 4;
    public int CurrentLife = 4;
    public float RunSpeed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float Luck;

    public int BaseMaxLife = 4;
    public float BaseRunSpeed;
    public float BaseAttackDamage;
    public float BaseAttackRange;
    public float BaseAttackSpeed;
    public float BaseLuck;
    public float BaseTPCoolDownSeconds;

    public float RunSpeedBonus;
    public float AttackDamageBonus;
    public float AttackRangeBonus;
    public float AttackSpeedBonus;
    public float LuckBonus;


    public bool Alive = true;
    int LifelineInProgress = 0;
    float iFrames = 60;
    int activeHearts = 0;
    int updateHeart = 0;
    int heartLife;
    float heartAnimDel = 1;
    public List<HeartAnim> UIHearts;
    public List<HeartAnim> GameHearts;
    public GameObject GameHeartParent;
    Animator MyAnim;
    PlayerInventory MyInv;

    public GameObject RetryButton;
    public RelicBonuses Relics;
    public GameObject LifelineImg;
    public FlashFade ScrFlash;
    public Sprite ShieldSpr;
    public Sprite MushroomSpr;
    public Sprite ScytheSpr;

    private void Awake()
    {
        GameInfo.PlayerStatus = this;
        Relics = GetComponent<RelicBonuses>();
        MyInv = GetComponent<PlayerInventory>();
    }
    void Start()
    {
        CurrentLife = MaxLife;
        UpdatePlayerStats();
        heartLife = MaxLife;
        ActivateHearts();
        MyAnim = GetComponent<Animator>();
        MyInv = GetComponent<PlayerInventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if (iFrames >= 0)
        {
            iFrames -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        else
        {
            if (GameHeartParent.activeSelf)
                GameHeartParent.SetActive(false);
            if (LifelineImg.activeSelf && Alive)
                LifelineImg.SetActive(false);
        }

        if (heartLife != CurrentLife)
        {
            UpdateHeartGFX();
            //   UpdateGameHeartGFX();
        }

        if (Input.GetKeyDown(KeyCode.K))
            DamagePlayer(1);
        if (Input.GetKeyDown(KeyCode.L))
            HealPlayer(1);

        if (!Alive)
        {
            if (Relics.Lifeline)
            {
                DoLifeline();
            }
        }

        ProcImgHandler();
    }

    void UpdateHeartGFX()
    {

        if (heartAnimDel > 0)
            heartAnimDel -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        else
        {
            if (heartLife < CurrentLife)
            {
                updateHeart = Mathf.CeilToInt((((float)heartLife + 1) / 2) - 1);
                if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Full)
                    heartAnimDel = 1;
                else
                {
                    if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Empty)
                    {
                        UIHearts[updateHeart].HalfHeart();
                        GameHearts[updateHeart].HalfHeart();
                    }
                    else if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Half)
                    {
                        UIHearts[updateHeart].FullHeart();
                        GameHearts[updateHeart].FullHeart();
                    }
                    heartLife += 1;
                    heartAnimDel = 20;
                }
            }
            if (heartLife > CurrentLife)
            {
                updateHeart = Mathf.CeilToInt((float)heartLife / 2f) - 1;
                if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Empty)
                    heartAnimDel = 1;
                else
                {
                    if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Full)
                    {
                        UIHearts[updateHeart].HalfHeart();
                        GameHearts[updateHeart].HalfHeart();
                    }
                    else if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Half)
                    {
                        UIHearts[updateHeart].EmptyHeart();
                        GameHearts[updateHeart].EmptyHeart();
                    }
                    heartLife -= 1;
                    heartAnimDel = 1;
                }


            }

        }
    }

    public void DamagePlayer(int _dmg)
    {

        if (CurrentLife > 0 && iFrames <= 0)
        {
            if (!Relics.Forcefield)
            {
                if ((Relics.ShieldBonus && Random.Range(0, 100) < 90 || !Relics.ShieldBonus))
                {
                    MyAnim.SetTrigger("Hurt");
                    CurrentLife -= _dmg;
                    iFrames = 60;
                    GameHeartParent.SetActive(true);
                    CamID.CMController.PPCam.assetsPPU += 2;
                }
                else
                {
                    LifelineImg.SetActive(true);
                    LifelineImg.transform.position = transform.position;
                    LifelineImg.GetComponent<SpriteRenderer>().sprite = ShieldSpr;
                    iFrames = 120;
                }
            }
            else
            {
                Relics.FField.SetActive(true);
                Relics.FField.GetComponent<AreaDamage>().Dmg = (int)AttackDamage * 3;
                MyInv.FindAndRemove(Item.RelicBonus.Forcefield);
                Relics.DetermineCurrentBonuses(MyInv.MyItems);
            }

        }
        if (CurrentLife <= 0 && Alive)
        {
            GameInfo.ForceCloseInv();
            CamID.CMController.dead = true;
            Alive = false;
            MyAnim.SetBool("Dead", true);
            if (!Relics.Lifeline)
                RetryButton.SetActive(true);
        }
    }

    public void HealPlayer(int _amt)
    {
        if (CurrentLife + _amt <= MaxLife)
        {
            CurrentLife += _amt;
            iFrames = 60;
            GameHeartParent.SetActive(true);
        }
        else
        {
            CurrentLife = MaxLife;
        }
    }

    public void ActivateHearts()
    {
        int index = 0;
        GameHeartParent.SetActive(true);
        for (int i = 2; i <= MaxLife; i += 2)
        {
            UIHearts[index].gameObject.SetActive(true);
            GameHearts[index].gameObject.SetActive(true);
            index++;
        }
        activeHearts = index - 1;
        GameHeartParent.transform.localPosition = new Vector3(1.2f - ((activeHearts - 1) * .4f), GameHeartParent.transform.localPosition.y);
        if (index < 5)
        {
            for (int i = index; i <= 4; i++)
            {
                UIHearts[i].gameObject.SetActive(false);
                GameHearts[i].gameObject.SetActive(false);
            }
        }
        UpdateHeartGFX();
        GameHeartParent.SetActive(false);
    }

    public void UpdatePlayerStats()
    {
        RunSpeedBonus = MyInv.CalcSpeed();
        AttackDamageBonus = MyInv.CalcDmg();
        AttackRangeBonus = MyInv.CalcRange();
        AttackSpeedBonus = MyInv.CalcROF();
        LuckBonus = MyInv.CalcLuck();

        DoApples(Relics.ApplesHeld);
        RunSpeed = RunSpeedBonus + BaseRunSpeed;
        AttackDamage = AttackDamageBonus + BaseAttackDamage;
        AttackRange = AttackRangeBonus + BaseAttackRange;
        AttackSpeed = AttackSpeedBonus + BaseAttackSpeed;
        Luck = LuckBonus + BaseLuck;

        Relics.DetermineCurrentBonuses(MyInv.MyItems);
    }

    /* void UpdateGameHeartGFX()
     {
         if (heartAnimDel > 0)
             heartAnimDel -= 60 * Time.deltaTime;
         else
         {
             if (heartLife < CurrentLife)
             {
                 updateHeart = Mathf.CeilToInt((((float)heartLife + 1) / 2) - 1);
                 if (GameHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Full)
                     heartAnimDel = 1;
                 else
                 {
                     if (GameHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Empty)
                         GameHearts[updateHeart].HalfHeart();
                     else if (GameHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Half)
                         GameHearts[updateHeart].FullHeart();
                     heartLife += 1;
                     heartAnimDel = 20;
                 }
             }
             if (heartLife > CurrentLife)
             {
                 updateHeart = Mathf.CeilToInt((float)heartLife / 2f) - 1;
                 if (GameHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Empty)
                     heartAnimDel = 1;
                 else
                 {
                     if (GameHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Full)
                         GameHearts[updateHeart].HalfHeart();
                     else if (UIHearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Half)
                         GameHearts[updateHeart].EmptyHeart();
                     heartLife -= 1;
                     heartAnimDel = 10;
                 }


             }

         }
     }*/

    void DoLifeline()
    {
        if (LifelineInProgress == 0)
        {
            LifelineInProgress = 1;
            LifelineImg.SetActive(true);
            LifelineImg.GetComponent<SpriteRenderer>().sprite = MushroomSpr;
            LifelineImg.transform.position = transform.root.position + new Vector3(0, 3, 0);
        }
        if (LifelineImg.transform.position.y > transform.root.position.y + .5f)
        {
            if (GameInfo.GM.GameSpeed == 1)
                LifelineImg.transform.position = Vector2.Lerp(LifelineImg.transform.position, transform.root.position, Time.deltaTime);
        }
        else if (LifelineInProgress == 1)
        {

            LifelineInProgress = 2;
            ScrFlash.GetComponent<SpriteRenderer>().color = ScrFlash.FlashScr;
            Alive = true;
            CurrentLife = MaxLife;
            LifelineInProgress = 0;
            CamID.CMController.dead = false;
            MyAnim.SetBool("Dead", false);
            LifelineImg.transform.position = transform.root.position + new Vector3(0, 3, 0);
            LifelineImg.SetActive(false);
            MyInv.FindAndRemove(Item.RelicBonus.Lifeline);
            Relics.DetermineCurrentBonuses(MyInv.MyItems);
            iFrames = 60 * activeHearts;
            GameHeartParent.SetActive(true);

        }
    }

    public void SetIFrames(float _frames)
    {
        iFrames = _frames;
    }

    public void DoApples(int _apples)
    {
        MaxLife = BaseMaxLife + 2 * _apples;
        ActivateHearts();
        UpdateHeartGFX();
        if (CurrentLife > MaxLife)
            CurrentLife = MaxLife;

    }

    void ProcImgHandler()
    {
        if (Alive && LifelineImg.activeSelf)
        {
            if (LifelineImg.transform.position.y < transform.root.position.y + 4f)
                LifelineImg.transform.position = Vector2.Lerp(LifelineImg.transform.position, transform.root.position + new Vector3(0, 6, 0), Time.deltaTime);
        }
    }
}
