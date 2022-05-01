using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonitor : MonoBehaviour
{

    public List<GameObject> EnemiesInTrigger = new List<GameObject>();
    PlayerSkills Player;
    // Start is called before the first frame update
    float precision = 10;
    int skillDmg = 0;
    float knockback = 0;

    private void Awake()
    {
        Player = transform.parent.GetComponent<PlayerSkills>();
    }
    private void Update()
    {
        if(precision>0)
        {
            precision -= 60 * Time.deltaTime;

            if(precision<=0)
            {
                Resolve();
                EnemiesInTrigger.Clear();
                gameObject.SetActive(false);
            }
        }
    }

    public void SetValues(float _prec, int _dmg, float _knockback)
    {
        precision = _prec;
        skillDmg = _dmg;
        knockback = _knockback;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && !EnemiesInTrigger.Contains(collision.gameObject))
            EnemiesInTrigger.Add(collision.gameObject);
    }
   

    public void Resolve()
    {
        if (EnemiesInTrigger.Count > 0)
        {
            foreach (GameObject targ in EnemiesInTrigger)
                targ.GetComponent<NPCStatus>().TakeDmg(skillDmg, knockback);
        }
    }
}
