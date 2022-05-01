using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchObject : MonoBehaviour
{
    float UpTraj = 0;
    Vector2 launchTraj;
    float rotAmt;
    bool launched = false;
    float decay = 60;

    // Update is called once per frame
    public void LaunchMe(Vector2 _traj, bool _rot)
    {
        launchTraj = _traj;
        launched = true;
        UpTraj = Random.Range(-1f, 3);
        if (_rot)
        rotAmt = Random.Range(-11, 11);
    }
    void Update()
    {
        if (launched)
        {
            if (UpTraj > -3)
            {
                transform.position += Vector3.ClampMagnitude((Vector3)launchTraj + new Vector3(0, UpTraj, 0), 2) * Time.deltaTime;
                UpTraj -= .1f * 60 * Time.deltaTime;
                transform.eulerAngles += new Vector3(0, 0, rotAmt * 60 * Time.deltaTime);
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                rotAmt = 0;
                if (decay > 0)
                    decay -= 60 * Time.deltaTime;
                else
                    gameObject.SetActive(false);
            }
        }
    }
}
