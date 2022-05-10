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
    float heartAnimDel = 20;
    public List<HeartAnim> Hearts;
    Animator MyAnim;
     PlayerInventory MyInv;

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

        if (heartLife != CurrentLife)
            UpdateHeartGFX();

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
                if (Hearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Full)
                    heartAnimDel = 1;
                else
                {
                    if (Hearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Empty)
                        Hearts[updateHeart].HalfHeart();
                    else if (Hearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Half)
                        Hearts[updateHeart].FullHeart();
                    heartLife += 1;
                    heartAnimDel = 20;
                }
            }
            if (heartLife > CurrentLife)
            {
                updateHeart = Mathf.CeilToInt((float)heartLife / 2f) - 1;
                if (Hearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Empty)
                    heartAnimDel = 1;
                else
                {
                    if (Hearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Full)
                        Hearts[updateHeart].HalfHeart();
                    else if (Hearts[updateHeart].HeartFilled == HeartAnim.HeartStatus.Half)
                        Hearts[updateHeart].EmptyHeart();
                    heartLife -= 1;
                    heartAnimDel = 20;
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
            //CamID.Cam.JarScreen(new Vector3(Random.Range(-.06f, .06f), Random.Range(-.06f, .06f),-10f));]
            //CamID.Cam.ZoomScreen(.05f);
           // CamID.Cam.DamageFlash(25);
        }
        if (CurrentLife<=0 && Alive)
        {
            Alive = false;
            MyAnim.SetBool("Dead", true);
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
        for (int i = 2; i <= MaxLife; i += 2)
        {
            Hearts[index].gameObject.SetActive(true);
            index++;
        }
        activeHearts = index;
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
}
