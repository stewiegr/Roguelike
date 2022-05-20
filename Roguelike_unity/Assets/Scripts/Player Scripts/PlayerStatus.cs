using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxLife = 8;
    public int CurrentLife = 8;
    public float RunSpeed;
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float Luck;

    public float BaseRunSpeed;
    public float BaseAttackDamage;
    public float BaseAttackRange;
    public float BaseAttackSpeed;
    public float BaseLuck;

    public float RunSpeedBonus;
    public float AttackDamageBonus;
    public float AttackRangeBonus;
    public float AttackSpeedBonus;
    public float LuckBonus;

    public bool Alive = true;
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

    private void Awake()
    {
        GameInfo.PlayerStatus = this;
        MyInv = GetComponent<PlayerInventory>();
    }
    void Start()
    {
        heartLife = CurrentLife;
        ActivateHearts();
        MyAnim = GetComponent<Animator>();
        MyInv = GetComponent<PlayerInventory>();
        UpdatePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(iFrames>=0)
        {
            iFrames -= 60 * Time.deltaTime;
        }
        else
        {
            if (GameHeartParent.activeSelf)
               GameHeartParent.SetActive(false);
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
    }

    void UpdateHeartGFX()
    {
       
        if (heartAnimDel > 0)
            heartAnimDel -= 60 * Time.deltaTime;
        else
        {
            if (heartLife < CurrentLife)
            {
                updateHeart = Mathf.CeilToInt((((float)heartLife+ 1)/2)-1);
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
                    heartAnimDel = 1;
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

        if (CurrentLife > 0 && iFrames<=0)
        {
            MyAnim.SetTrigger("Hurt");
            CurrentLife -= _dmg;
            iFrames = 60;
            GameHeartParent.SetActive(true);
            CamID.CMController.PPCam.assetsPPU += 2;

        }
        if (CurrentLife<=0 && Alive)
        {
            CamID.CMController.dead = true;
            Alive = false;
            MyAnim.SetBool("Dead", true);
            RetryButton.SetActive(true);
        }
    }

    public void HealPlayer(int _amt)
    {
        if (CurrentLife + _amt <= MaxLife)
        {
            CurrentLife += _amt;
        }
        else
        {
            CurrentLife = MaxLife;
        }
    }

    void ActivateHearts()
    {
        int index = 0;
        GameHeartParent.SetActive(true);
        for (int i = 2; i <= MaxLife; i += 2)
        {
            UIHearts[index].gameObject.SetActive(true);
            GameHearts[index].gameObject.SetActive(true);
            index++;
        }
        activeHearts = index;
        GameHeartParent.transform.localPosition = new Vector3(GameHeartParent.transform.localPosition.x + (.8f * (5 - MaxLife / 2)), GameHeartParent.transform.localPosition.y); ;
        GameHeartParent.SetActive(false);
    }

    public void UpdatePlayerStats()
    {
        RunSpeedBonus = MyInv.CalcSpeed();
        AttackDamageBonus = MyInv.CalcDmg();
        AttackRangeBonus = MyInv.CalcRange();
        AttackSpeedBonus = MyInv.CalcROF();
        LuckBonus = MyInv.CalcLuck();

        RunSpeed = RunSpeedBonus + BaseRunSpeed;
        AttackDamage = AttackDamageBonus + BaseAttackDamage;
        AttackRange = AttackRangeBonus + BaseAttackRange;
        AttackSpeed = AttackSpeedBonus + BaseAttackSpeed;
        Luck = LuckBonus + BaseLuck;
    }

    void UpdateGameHeartGFX()
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
                    //heartAnimDel = 10;
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
                    //heartAnimDel = 10;
                }


            }
            
        }
    }
}
