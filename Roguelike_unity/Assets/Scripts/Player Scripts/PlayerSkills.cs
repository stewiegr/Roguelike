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
    float atkDly = 20;
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
        if (MyStatus.Alive)
        {
            if (Input.GetMouseButton(0) && atkDly<=0)// || (Gamepad.current.rightTrigger.wasPressedThisFrame))
            {
                atkDly = 15;
                myAnim.Play("PlayerAttack");
                DoBasicProjectile();
            }
        }
    }

    void DoBasicProjectile()
    {
        GameObject proj = Instantiate(BasicProjectile, transform.position + (transform.right * .1f) + Vector3.up*.15f, transform.localRotation);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.TargetEnemy = true;
        BP.dmg = 1;


        proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * 7.5f;


    }

    void DoBasicAtk()
    {
        //NearCircle.gameObject.SetActive(true);
    }

    public void ReceiveTargets(List<GameObject> Targets)
    {

    }
}
