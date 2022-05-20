using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public GameManager GM;

    private void OnMouseDown()
    {
        GM.ResetLevel();
    }
}
