using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : MonoBehaviour
{
    public NPCStatus MyStatus;
    public HomingAI MyNav;
    public Animator MyAnim;

    float atkDly = 30;
    public int MyDmg = 1;
    public float AtkRange = 5;
    public GameObject MyProjectile;
    public Transform LaunchPointPosition;

    public int AtkSfxIndex;
    private void Start()
    {
        GetComponent<HomingAI>().SetAtkRange(AtkRange);
    }
    void Update()
    {
        if (MyStatus.AtkDly > 0)
        {
            atkDly -= 60 * Time.deltaTime * GameInfo.GM.GameSpeed;
        }
        if (MyNav.InAtkRange && MyStatus.Alive)
        {
            MyAnim.SetBool("Idle", true);
            if(atkDly<=0 && MyNav.FreeMove)
            {
                MyAnim.SetTrigger("Attack");
                atkDly = MyStatus.AtkDly;
                //SpawnProjectile();
            }
        }
        else if(!MyNav.InAtkRange)
            MyAnim.SetBool("Idle", false);
    }

    public void SpawnProjectile()
    {
        GameInfo.PlayAudio(AtkSfxIndex);
        Vector2 direction = GameInfo.Player.position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        //transform.eulerAngles = new Vector3(0, 0, angle);
        GameObject proj = Instantiate(MyProjectile, LaunchPointPosition.position, transform.localRotation);
        //proj.transform.eulerAngles = new Vector3(0, 0, angle);
        BasicProjectile BP = proj.GetComponent<BasicProjectile>();
        BP.life = 400;
        BP.TargetEnemy = false;
        BP.TargetPlayer = true;
        BP.dmg = 1;
        proj.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(direction) * 4f;
    }


}
