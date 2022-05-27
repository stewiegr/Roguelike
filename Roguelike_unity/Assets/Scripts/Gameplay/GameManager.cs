using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentKillsThisWave = 0;
    public int LivingEnemies = 0;
    public GameObject InventoryWindow;

    public List<LevelManager> Levels;
    public LevelManager CurrentLevel;
    public List<GameObject> TemporaryDebris = new List<GameObject>();
    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GameInfo.GM = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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

    public void ResetLevel()
    {
        CurrentLevel.ResetLevel();
    }
}
