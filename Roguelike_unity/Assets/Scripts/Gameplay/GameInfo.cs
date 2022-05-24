using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo
{
    public static Transform Player;
    public static PlayerStatus PlayerStatus;
    public static Vector2 PlayerPos;
    public static GameManager GM;
    public static ItemDatabase ItemDB;
    public static GameObject ItemOnCursor;
    public static bool PlayerInMenu = false;
    public static GameObject ItemInfoWindow;
    public static Transform ZoomOffset;
    public static Transform Inv;
    public static EffectsDB EffectsDB;

    public static void PositionInv()
    {
        PlayerInventory inv = Player.GetComponent<PlayerInventory>();
        Inv.transform.position = new Vector3(ZoomOffset.position.x + 2, ZoomOffset.position.y, Inv.position.z);
        for (int i = 0; i <= 15; i++)
        {
            //inv.Squares[i].HomePos = inv.Squares[i].transform.localPosition;
        }
    }
}
