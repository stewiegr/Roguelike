using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

public class PlayerSkills : MonoBehaviour
{
    Animator myAnim;
    public GameObject NearCircle;
    public GameObject BasicProjectile;
    List<GameObject> Targets = new List<GameObject>(0);
    float atkDly = 30;
    float autofireDetect = 0;
    PlayerStatus MyStatus;
    PlayerController MyController;
    PlayerInventory MyInv;
    public bool ShakeOnFire = true;
    bool autoFire = false;

    int[] multiShotAngles = { 3, -3, 6, -6, 9, -9, 12, -12, 15, -15, 18, -18 };

    void Start()
    {
        myAnim = transform.parent.GetComponent<Animator>();
        MyStatus = transform.parent.GetComponent<PlayerStatus>();
        MyController = transform.parent.GetComponent<PlayerController>();
        MyInv = transform.parent.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atkDly > 0)
            atkDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        if (MyStatus.Alive && !GameInfo.PlayerInMenu && GameInfo.GM.GameSpeed != 0)
        {
            if ((Input.GetMouseButton(0) || autoFire) && atkDly <= 0)// || (Gamepad.current.rightTrigger.wasPressedThisFrame))
            {
                atkDly = 40 - (20 * (MyStatus.AttackSpeed / 10));
                myAnim.Play("PlayerAttack");
                DoBasicProjectile();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (autofireDetect > 0)
            {
                autoFire = true;
                MyController.CDAlert.gameObject.SetActive(true);
                MyController.CDAlert.GetComponent<AutoDisableObject>().ActivateObj(90);
                MyController.CDAlert.text = "Autofire ON";
                
            }
            else if (!autoFire)
                autofireDetect = 10;
        }
        if (autofireDetect > 0)
            autofireDetect -= 60 * Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            if (autofireDetect <= 0 && autoFire)
            {
                autoFire = false;
                MyController.CDAlert.gameObject.SetActive(true);
                MyController.CDAlert.GetComponent<AutoDisableObject>().ActivateObj(90);
                MyController.CDAlert.text = "Autofire OFF";
            }
            }


    }

    void DoBasicProjectile()
    {
        if (ShakeOnFire)
        {
            CamID.CMController.ShakeScreen(.75f, 1);
        }

        GameInfo.PlayAudio(5);
        GameObject proj = Instantiate(MyInv.Weapon.GameItem.StaffProjectile, transform.position + Vector3.up * .35f + transform.right * .85f, transform.parent.rotation);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        //BP.life = BP.life * MyStatus.AttackRange;
        BP.TargetEnemy = true;
        BP.dmg = (int)MyStatus.AttackDamage;
        if (BP.DefaultVel == 0)
            proj.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(transform.right) * 10.5f;
        else
            proj.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(transform.right) * BP.DefaultVel;
        if (MyStatus.Relics.PenetratingProjectile>0)
            BP.Penetrations = MyStatus.Relics.SetProjectilePenetrations() * BP.PenetrationMultiplier;

        if (MyStatus.Relics.TrackingShots>0)
        {
            BP.AcquireTarget();
        }


        if (MyStatus.Relics.TripleShot>0)
        {
            for (int i = 0; i <= MyStatus.Relics.TripleShot; i++)
            {
                GameObject proj1 = Instantiate(MyInv.Weapon.GameItem.StaffProjectile, transform.position + Vector3.up * .35f, transform.parent.rotation);
                BP = proj1.GetComponent<BasicProjectile>();
               // BP.life = BP.life * MyStatus.AttackRange;
                BP.TargetEnemy = true;
                BP.dmg = (int)MyStatus.AttackDamage;
                if (MyStatus.Relics.PenetratingProjectile > 0)
                    BP.Penetrations = Random.Range(3, 5) * BP.PenetrationMultiplier;
                if (BP.DefaultVel == 0)
                    proj1.GetComponent<Rigidbody2D>().velocity = (Vector3.Normalize(transform.right) * 10.5f) + transform.up * multiShotAngles[i];
                else
                    proj1.GetComponent<Rigidbody2D>().velocity = (Vector3.Normalize(transform.right) * BP.DefaultVel) + transform.up * multiShotAngles[i];

                if (MyStatus.Relics.TrackingShots > 0)
                {
                    BP.AcquireTarget();
                }
            }
            /*
            GameObject proj2 = Instantiate(MyInv.Weapon.GameItem.StaffProjectile, transform.position + Vector3.up * .35f, transform.root.rotation);
           // proj2.transform.Rotate(0, 0, -25);
            BP = proj2.GetComponent<BasicProjectile>();
            BP.life = BP.life * MyStatus.AttackRange;
            BP.TargetEnemy = true;
            BP.dmg = (int)MyStatus.AttackDamage;
            if (MyStatus.Relics.PenetratingProjectile>0)
                BP.Penetrations = Random.Range(3, 5) * BP.PenetrationMultiplier;
            if (BP.DefaultVel == 0)
                proj2.GetComponent<Rigidbody2D>().velocity = (Vector3.Normalize(transform.right) * 10.5f) + transform.up * -3f;
            else
                proj2.GetComponent<Rigidbody2D>().velocity = (Vector3.Normalize(transform.right) * BP.DefaultVel) + transform.up * -3f;

            if (MyStatus.Relics.TrackingShots>0)
            {
                BP.AcquireTarget();
                BP.HomingRate = MyStatus.Relics.TrackingShots;
            }*/
        }


    }

    void DoBasicAtk()
    {
        //NearCircle.gameObject.SetActive(true);
    }

    public void ReceiveTargets(List<GameObject> Targets)
    {

    }
    public void AtkDlySet(float _frames)
    {
        atkDly = _frames;
    }
}
