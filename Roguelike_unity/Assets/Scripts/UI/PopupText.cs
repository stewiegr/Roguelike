using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public TextMeshPro txt;
    float riseSpeed = 4;
    float sideVel = 0;

    public void InitTextCustom(string _tx, float _rs, Color _col, float _sz)
    {
        txt.color = _col;
        txt.text = _tx;
        riseSpeed = _rs;
        txt.fontSize = _sz;
        sideVel = Random.Range(-1, 1);
        riseSpeed += (Random.Range(-1.5f, 1.5f));
    }

    private void Update()
    {
        transform.Translate(new Vector2(sideVel * Time.deltaTime, riseSpeed * Time.deltaTime));
        riseSpeed = Mathf.Lerp(riseSpeed, 0, 2 *Time.deltaTime);
        if (riseSpeed <= .25f)
            GameObject.Destroy(this.gameObject);
    }
}
