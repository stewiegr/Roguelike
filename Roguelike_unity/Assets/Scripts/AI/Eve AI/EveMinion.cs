using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveMinion : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Eve;
    NPCStatus MyStatus;
    Animator MyAnim;
    Rigidbody2D myRB;
    float moving;
    float pausing;
    float speed;
    void Start()
    {
        moving = Random.Range(30, 60);
        myRB = GetComponent<Rigidbody2D>();
        speed = Random.Range(1.5f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(moving>0)
        {
            moving -= 60 * Time.deltaTime;
            myRB.velocity = -Vector2.ClampMagnitude(((Vector2)transform.position - (Vector2)Eve.position), speed);

            if(Vector2.Distance(transform.position,Eve.position)<1)
            {
                Eve.GetComponent<NPCStatus>().Heal(30);
                GameObject.Destroy(this.gameObject);
            }

        }
        else
        {
            myRB.velocity = Vector2.zero;
            if(pausing<=0)
            pausing = Random.Range(30, 60);
        }
        if(pausing>0)
        {
            pausing -= 60 * Time.deltaTime;
            if (pausing <= 0)
                moving = Random.Range(30, 60);
        }
    }

    private void OnDestroy()
    {
        if(Eve!=null)
        Eve.GetComponent<EveAI>().SpawnedMinions.Remove(this.gameObject);
    }
}
