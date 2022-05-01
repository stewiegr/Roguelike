using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugTools : MonoBehaviour
{

    public Transform Enemies;
    public TextMeshProUGUI Ct;
    public TextMeshProUGUI FPS;
    int ECt;
    public float deltaTime;
    float FPSupd = 60;

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.P))
        {
            Instantiate(Enemies, new Vector3(Random.Range(-4,8),Random.Range(-4,6),-1), transform.rotation);
            ECt++;
            Ct.text = "Enemy Count: " + ECt.ToString();
        }
    }

    private void Update()
    {
        if (FPSupd > 0)
        {
            FPSupd -= 60 * Time.deltaTime;

            if (FPSupd <= 0)
            {
                FPSupd = 60;
                deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
                float fps = 1.0f / deltaTime;
                FPS.text = "FPS: " + Mathf.Ceil(fps).ToString();
            }
        }
    }
}
