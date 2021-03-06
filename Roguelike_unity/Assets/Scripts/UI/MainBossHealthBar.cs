using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainBossHealthBar : MonoBehaviour
{

    public Image HealthActual;
    public Image HealthLag;
    public TextMeshProUGUI BossName;
    Rigidbody2D MyRb;
    RectTransform HBRed;
    RectTransform HBYellow;
    NPCStatus Boss;
    float decay = 180;

    public void InitHealthBar(NPCStatus _Boss, string _name)
    {
        BossName.text = _name;
        HBRed = HealthActual.transform as RectTransform;
        HBYellow = HealthLag.transform as RectTransform;
        Boss = _Boss;
        MyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss != null)
        {
            HBRed.sizeDelta = new Vector2(((float)Boss.Life / (float)Boss.maxLife) * 500, 5);
            HBYellow.sizeDelta = Vector2.Lerp(new Vector2(HBYellow.rect.width, HBYellow.rect.height), new Vector2(((float)Boss.Life / (float)Boss.maxLife) * 500, 5), 10 * Time.deltaTime);
        }
        else
        {
            if(!MyRb.IsAwake())
            {
                MyRb.WakeUp();
            }
            transform.eulerAngles += new Vector3(0, 0, .33f);
            decay -= 60 * Time.deltaTime;
            if(decay<=0)
            {
                GameObject.Destroy(this.gameObject);
            }    
        }
    }
}
