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
    void Start()
    {
        myAnim = transform.root.GetComponent<Animator>();
        MyStatus = transform.root.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atkDly > 0)
            atkDly -= 60 * Time.deltaTime;
        if (MyStatus.Alive && !GameInfo.PlayerInMenu)
        {
            if (Input.GetMouseButton(0) && atkDly<=0)// || (Gamepad.current.rightTrigger.wasPressedThisFrame))
            {
                atkDly = 30-MyStatus.AttackSpeed;
                myAnim.Play("PlayerAttack");
                DoBasicProjectile();
            }
        }
    }

    void DoBasicProjectile()
    {
        GameObject proj = Instantiate(BasicProjectile, transform.position + Vector3.up*.35f, transform.localRotation);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = BP.life * MyStatus.AttackRange;
        BP.TargetEnemy = true;
        BP.dmg = (int)MyStatus.AttackDamage;
        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 10.5f;
        if (MyStatus.Relics.PenetratingProjectile)
            BP.Penetrations = Random.Range(3, 5);

        if(MyStatus.Relics.TripleShot)
        {
            GameObject proj1 = Instantiate(BasicProjectile, transform.position + Vector3.up * .35f, transform.localRotation);
            proj1.transform.Rotate(0, 0, 25);
            BP = proj1.GetComponent<BasicProjectile>();
            BP.life = BP.life * MyStatus.AttackRange;
            BP.TargetEnemy = true;
            BP.dmg = (int)MyStatus.AttackDamage;
            if (MyStatus.Relics.PenetratingProjectile)
                BP.Penetrations = Random.Range(3, 5);
            proj1.GetComponent<Rigidbody2D>().velocity = proj1.transform.right * 10.5f;
            GameObject proj2 = Instantiate(BasicProjectile, transform.position + Vector3.up * .35f, transform.localRotation);
            proj2.transform.Rotate(0, 0, -25);
            BP = proj2.GetComponent<BasicProjectile>();
            BP.life = BP.life * MyStatus.AttackRange;
            BP.TargetEnemy = true;
            BP.dmg = (int)MyStatus.AttackDamage;
            if (MyStatus.Relics.PenetratingProjectile)
                BP.Penetrations = Random.Range(3, 5);
            proj2.GetComponent<Rigidbody2D>().velocity = proj2.transform.right * 10.5f;
        }


    }

    void DoBasicAtk()
    {
        //NearCircle.gameObject.SetActive(true);
    }

    public void ReceiveTargets(List<GameObject> Targets)
    {

    }
}
