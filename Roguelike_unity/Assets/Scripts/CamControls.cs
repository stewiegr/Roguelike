using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamControls : MonoBehaviour
{
    // Start is called before the first frame update

    float ShakeDur;
    float ShakeAmt;
    float LerpHome = 0;
    Vector3 JarDist;
    Vector3 HomePos;
    float homeZoom;
    float zoomDist = 0;
    public Image RedFlash;
    Color32 redflashCol;

    private void Start()
    {
        HomePos = transform.position;
        ShakeDur = 0;
        ShakeAmt = 0;
        CamID.Cam = this;
        CamID.GameCam = GetComponent<Camera>();
        JarDist.x = 0;
        JarDist.y = 0;
        homeZoom = GetComponent<Camera>().orthographicSize;
        redflashCol = RedFlash.color;
    }

    public void DamageFlash(byte alpha)
    {
        redflashCol.a = alpha;
        RedFlash.color = redflashCol;
    }
    public void ShakeScreen(float _amt, float _dur)
    {
        ShakeDur = _dur;
        ShakeAmt = _amt;
    }

    public void JarScreen(Vector3 _amt)
    {
        JarDist = new Vector3(_amt.x, _amt.y, -10);
        transform.position = HomePos + JarDist;
    }

    public void ZoomScreen(float _amt)
    {
        CamID.GameCam.orthographicSize += _amt;
        zoomDist = _amt;
    }

    private void FixedUpdate()
    {
        if (ShakeDur > 0)
            DoShake();
        if (transform.position != HomePos)
            DoJar();
        if (zoomDist != 0)
            DoZoom();
        if (redflashCol.a > 0)
            DoFlash();
        else
        {
            redflashCol.a = 0;
            RedFlash.color = redflashCol;
        }    
    }

    void DoFlash()
    {
        redflashCol.a -= 3;
        if (redflashCol.a < 0 || redflashCol.a > 245)
            redflashCol.a = 0;
        RedFlash.color = redflashCol;
            
    }

    void DoZoom()
    {
        zoomDist -= .005f;
        CamID.GameCam.orthographicSize = homeZoom + zoomDist;
        if (zoomDist < 0)
            zoomDist = 0;
    }

    void DoJar()
    {       
        transform.position = Vector3.Lerp(transform.position, HomePos, .1f);
    }

    void DoShake()
    {
        {
            ShakeDur--;
            transform.eulerAngles = new Vector3(0, 0, Random.Range(-ShakeAmt, ShakeAmt));

            if (ShakeDur <= 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
}
