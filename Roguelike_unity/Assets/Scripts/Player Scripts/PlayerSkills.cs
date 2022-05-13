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


    }

    void DoBasicAtk()
    {
        //NearCircle.gameObject.SetActive(true);
    }

    public void ReceiveTargets(List<GameObject> Targets)
    {

    }
}
