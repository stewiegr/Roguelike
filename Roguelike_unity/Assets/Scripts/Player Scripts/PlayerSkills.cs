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
    bool resetTrigger = true;
    void Start()
    {
        myAnim = transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))// || (Gamepad.current.rightTrigger.wasPressedThisFrame))
        {
            myAnim.Play("PlayerAttack");
            DoBasicProjectile();
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
        NearCircle.gameObject.SetActive(true);
        NearCircle.GetComponent<TriggerMonitor>().SetValues(3, 3, 7);
    }

    public void ReceiveTargets(List<GameObject> Targets)
    {

    }
}
