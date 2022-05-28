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
    PlayerStatus MyStatus;
    PlayerInventory MyInv;
    void Start()
    {
        myAnim = transform.root.GetComponent<Animator>();
        MyStatus = transform.root.GetComponent<PlayerStatus>();
        MyInv = transform.root.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atkDly > 0)
            atkDly -= 60 * Time.deltaTime;
        if (MyStatus.Alive && !GameInfo.PlayerInMenu)
        {
            if (Input.GetMouseButton(0) && atkDly <= 0)// || (Gamepad.current.rightTrigger.wasPressedThisFrame))
            {
                atkDly = 40 - (20 * (MyStatus.AttackSpeed/10));
                myAnim.Play("PlayerAttack");
                DoBasicProjectile();
            }
        }
    }

    void DoBasicProjectile()
    {
        GameObject proj = Instantiate(MyInv.Weapon.GameItem.StaffProjectile, transform.position + Vector3.up * .35f, transform.localRotation);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = BP.life * MyStatus.AttackRange;
        BP.TargetEnemy = true;
        BP.dmg = (int)MyStatus.AttackDamage;
        if (BP.DefaultVel == 0)
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 10.5f;
        else
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * BP.DefaultVel;
        if (MyStatus.Relics.PenetratingProjectile)
            BP.Penetrations = Random.Range(3, 5) * BP.PenetrationMultiplier;

        if(MyStatus.Relics.TrackingShots)
        {
            BP.AcquireTarget();
        }


        if (MyStatus.Relics.TripleShot)
        {
            GameObject proj1 = Instantiate(MyInv.Weapon.GameItem.StaffProjectile, transform.position + Vector3.up * .35f, transform.localRotation);
            proj1.transform.Rotate(0, 0, 25);
            BP = proj1.GetComponent<BasicProjectile>();
            BP.life = BP.life * MyStatus.AttackRange;
            BP.TargetEnemy = true;
            BP.dmg = (int)MyStatus.AttackDamage;
            if (MyStatus.Relics.PenetratingProjectile)
                BP.Penetrations = Random.Range(3, 5) * BP.PenetrationMultiplier; 
            if (BP.DefaultVel == 0)
                proj1.GetComponent<Rigidbody2D>().velocity = proj1.transform.right * 10.5f;
            else
                proj1.GetComponent<Rigidbody2D>().velocity = proj1.transform.right * BP.DefaultVel;

            if (MyStatus.Relics.TrackingShots)
            {
                BP.AcquireTarget();
            }

            GameObject proj2 = Instantiate(MyInv.Weapon.GameItem.StaffProjectile, transform.position + Vector3.up * .35f, transform.localRotation);
            proj2.transform.Rotate(0, 0, -25);
            BP = proj2.GetComponent<BasicProjectile>();
            BP.life = BP.life * MyStatus.AttackRange;
            BP.TargetEnemy = true;
            BP.dmg = (int)MyStatus.AttackDamage;
            if (MyStatus.Relics.PenetratingProjectile)
                BP.Penetrations = Random.Range(3, 5) * BP.PenetrationMultiplier; 
            if (BP.DefaultVel == 0)
                proj2.GetComponent<Rigidbody2D>().velocity = proj2.transform.right * 10.5f;
            else
                proj2.GetComponent<Rigidbody2D>().velocity = proj2.transform.right * BP.DefaultVel;

            if (MyStatus.Relics.TrackingShots)
            {
                BP.AcquireTarget();
            }
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
