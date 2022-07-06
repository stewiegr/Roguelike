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
    public TextMeshPro GoldUI;
    public GameObject WorldObjects;
    public int Gold = 0;
    public List<GameObject> LevelEnvironments;
    private void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        GameInfo.GM = this;
    }

    private void Start()
    {
        BeginLevel(0);
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

    public void BeginLevel(int _index)
    {
        CurrentLevel = Instantiate(Levels[_index]);
        CurrentLevel.transform.SetParent(transform.root);
        for(int i=0; i<LevelEnvironments.Count; i++)
        {
            if (CurrentLevel.EnvironmentIndex == i)
            {
                LevelEnvironments[i].SetActive(true);
                CamID.CMController.GameArea = LevelEnvironments[i];
            }
            else
                LevelEnvironments[i].SetActive(false);
        }

    }

    public void StopLevel()
    {
        CurrentLevel.Environment.SetActive(false);
        GameObject.Destroy(CurrentLevel);
    }

    public void ResetLevel()
    {
        CurrentLevel.ResetLevel();
    }

    public void AddGold(int _amt)
    {
        Gold += _amt;
    }
}
