using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvID : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Pos;

    public TextMeshPro DmgTxt;
    public TextMeshPro MoveSpeedTxt;
    public TextMeshPro AtkSpdTxt;
    public TextMeshPro AtkRangeTxt;
    public TextMeshPro LuckTxt;
    void Awake()
    {
        GameInfo.Inv = this.transform;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp((Vector2)transform.position,(Vector2)Pos.position + Vector2.right * 2, GameInfo.PlayerStatus.RunSpeed*Time.deltaTime);

        DmgTxt.text = GameInfo.PlayerStatus.AttackDamage.ToString();
        MoveSpeedTxt.text = GameInfo.PlayerStatus.RunSpeed.ToString();
        AtkSpdTxt.text = GameInfo.PlayerStatus.AttackSpeed.ToString();
        AtkRangeTxt.text = GameInfo.PlayerStatus.AttackRange.ToString();
        LuckTxt.text = GameInfo.PlayerStatus.Luck.ToString();

    }

}