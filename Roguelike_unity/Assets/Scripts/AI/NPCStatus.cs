using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatus : MonoBehaviour
{
    public Animator MyAnim;
    public HomingAI MyNav;
    // Start is called before the first frame update

    public int Life;
    public int AtkDmg;
    public float AtkRange;
    public float AtkDly = 20;
    public float preventAtk = 0;
    public bool Alive = true;
    public List<GameObject> Gibs;
    public float RunSpeed;

    private void Start()
    {
        if(Gibs.Count>0)
        {
            for(int i =0; i<Gibs.Count; i++)
            {
                Gibs[i] = Instantiate(Gibs[i]);
                Gibs[i].SetActive(false);
            }
        }
    }

    public void TakeDmg(int _dmg, float _knockback)
    {
        MyAnim.SetTrigger("Hurt");
        Life -= _dmg;
        if (Life > 0)
            MyNav.Knockback(_knockback);
        else
        {
            Die();
        }
    }

    void Die()
    {
        Alive = false;
        GetComponent<Collider2D>().enabled = false;

        if (Random.Range(0, 10) > 7)
        {
            if(Gibs.Count>0)
            {
                for(int i = 0; i < Gibs.Count; i++)
                {
                    Gibs[i].transform.position = this.transform.position;
                    Gibs[i].SetActive(true);
                    Gibs[i].GetComponent<LaunchObject>().LaunchMe(new Vector2(Random.Range(-2, 2), Random.Range(-1, 3)),true);
                    Gibs[i].GetComponent<SpriteRenderer>().sortingLayerName = "Top Down";
                }
            }
            gameObject.SetActive(false);
            CamID.Cam.ShakeScreen(1, 4);
        }
        else
        {
            MyAnim.SetTrigger("Die");
        }
    }
}
