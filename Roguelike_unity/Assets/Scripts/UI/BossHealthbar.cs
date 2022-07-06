using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthbar : MonoBehaviour
{
    public Transform HealthBar;
    float healthBarFull = .499f;
    float healthBarEmpty = -.255f;
    //.754 distance


    public void SetHealthBar(int _currentLife, int _maxLife)
    {
        HealthBar.localPosition = new Vector2(HealthBar.localPosition.x, (healthBarFull - (healthBarFull - healthBarEmpty - ((healthBarFull - healthBarEmpty) * _currentLife / _maxLife))));
    }
}
