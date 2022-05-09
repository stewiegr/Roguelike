using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentKillsThisWave = 0;
    public int LivingEnemies = 0;
    public GameObject InventoryWindow;
    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GameInfo.GM = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryWindow.SetActive(!InventoryWindow.activeSelf);
            if(InventoryWindow.activeSelf)
                GameInfo.PositionInv();
            {
                if (!GameInfo.PlayerInMenu)
                    GameInfo.PlayerInMenu = true;
                else
                {
                    GameInfo.Player.GetComponent<PlayerInventory>().CloseInv();
                    GameInfo.PlayerInMenu = false;
                    GameInfo.ItemInfoWindow.SetActive(false);
                }
            }
        }
    }
}
