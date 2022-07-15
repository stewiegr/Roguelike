using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 mousePosition;
    Vector3 Rot;
    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public Vector3 GetMousePos()
    {
        return mousePosition;
    }
}
