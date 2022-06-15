using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentKillsThisWave = 0;
    public int LivingEnemies = 0;
    public GameObject InventoryWindow;

    public List<LevelManager> Levels;
    public LevelManager CurrentLevel;
    public List<GameObject> TemporaryDebris = new List<GameObject>();
    public float GameSpeed = 1;
    public GameObject PauseFade;
    public List<BoxCollider2D> Abyss;
    public TextMeshProUGUI GoldUI;
    public GameObject WorldObjects;
    public int Gold = 0;
    private void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GameInfo.GM = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GameSpeed==1)
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
                    if (GameInfo.CurrentChest != null)
                        GameInfo.CurrentChest.CloseChest();
                    GameInfo.ItemInfoWindow.SetActive(false);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameSpeed == 1)
            {
                GameSpeed = 0;
                PauseFade.SetActive(true);
                WorldObjects.transform.position += new Vector3(0, 0, 99);
            }
            else
            {
                GameSpeed = 1;
                PauseFade.SetActive(false);
                WorldObjects.transform.position += new Vector3(0, 0, -99);
            }
        }
    }

    public void ResetLevel()
    {
        CurrentLevel.ResetLevel();
    }

    public void AddGold(int _amt)
    {
        Gold += _amt;
        GoldUI.text = "Gold: " + Gold.ToString();
    }
}
