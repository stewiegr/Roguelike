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
    public static ChestInteract CurrentChest;
    public static SFXHandler SFX;
    public static UIDB UIDB;

    public static void PositionInv()
    {
        PlayerInventory inv = Player.GetComponent<PlayerInventory>();
        Inv.transform.position = new Vector3(ZoomOffset.position.x + 2, ZoomOffset.position.y, Inv.position.z);
        for (int i = 0; i <= 15; i++)
        {
            //inv.Squares[i].HomePos = inv.Squares[i].transform.localPosition;
        }
    }

    public static void ForceCloseInv()
    {
        if (GM.InventoryWindow.activeSelf)
        {
            Player.GetComponent<PlayerInventory>().CloseInv();
            PlayerInMenu = false;
            ItemInfoWindow.SetActive(false);
            GM.InventoryWindow.SetActive(false);
        }
        if(CurrentChest!=null)
        {
            CurrentChest.CloseChest();
        }
    }

    public static void PlayAudio(int index)
    {
        SFX.PlayAudioClip(index);
    }
}
