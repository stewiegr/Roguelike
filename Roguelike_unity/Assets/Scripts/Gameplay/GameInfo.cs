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
}
