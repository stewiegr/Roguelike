using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro Prompt;
    bool open = false;
    float animDel = 20;
    public List<GameObject> Squares;
    public List<Vector2> SquareCoords = new List<Vector2>();
    bool allOpen = false;
    bool allAssigned = false;
    bool delayClose = false;

    ChestInventory Inv;

    void Start()
    {
        for (int i = 0; i <= 8; i++)
            SquareCoords.Add((Vector2)Squares[i].transform.position);

        Inv = transform.root.GetComponent<ChestInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Prompt.gameObject.activeSelf)
            {
                GameInfo.PlayAudio(4);
                transform.root.GetComponent<Animator>().Play("ChestOpen");
                OpenChest();
            }
        }

        //DEBUG
        if (allOpen)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Inv.ShuffleLoot();
                AssignItems();
            }
        }

        if (open)
        {
            #region OldChestOpen
            /* if (animDel <= 0)
             {
                 if(!allOpen)
                 {
                     for (int i = 0; i <= 8; i++)
                     {
                         Squares[i].SetActive(true);
                         Squares[i].GetComponent<InvSlot>().MyChest = Inv;
                         Squares[i].GetComponent<InvSlot>().IndexInInv = i;
                     }
                     allOpen = true;
                     GameInfo.PlayerInMenu = true;
                     GameInfo.GM.InventoryWindow.SetActive(true);
                     GameInfo.PositionInv();
                     AssignItems();
                 }
             }
             else
             {
                     animDel -= 60 * Time.deltaTime;
                     if (Random.Range(0, 20) > animDel)
                     {
                         Squares[Random.Range(0, 9)].SetActive(true);
                     }             
             }*/
            #endregion
            if (!allOpen)
            {
                int open = 0;
                for (int i = 0; i <= 8; i++)
                {
                    if (Vector2.Distance(Squares[i].transform.position, SquareCoords[i]) > .2f)
                    {
                        Squares[i].transform.position = Vector2.MoveTowards(Squares[i].transform.position, SquareCoords[i], 20 * Time.deltaTime);
                    }
                    else
                    {
                        Squares[i].transform.position = SquareCoords[i];
                        open++;
                    }
                }
                if (open == 9)
                {
                    for (int i = 0; i <= 8; i++)
                    {
                        Squares[i].GetComponent<InvSlot>().MyChest = Inv;
                        Squares[i].GetComponent<InvSlot>().IndexInInv = i;
                    }
                    GameInfo.PlayerInMenu = true;
                    GameInfo.GM.InventoryWindow.SetActive(true);
                    GameInfo.PositionInv();
                    allOpen = true;
                    AssignItems();
                }
            }

        }
        if (delayClose)
        {
            if (allAssigned)
                CloseChest();
        }

        if (open)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameInfo.PlayerInMenu = false;
                GameInfo.GM.InventoryWindow.SetActive(false);
                CloseChest();
                Prompt.gameObject.SetActive(true);
            }
        }


    }
    void AssignItems()
    {
        int index = 0;
        foreach (Item item in Inv.MyItems)
        {
            Squares[index].GetComponent<InvSlot>().UpdateSlot(item);
            index++;
        }
        if (index < 9)
        {
            for (int i = index; i < 9; i++)
            {
                Squares[index].GetComponent<InvSlot>().ClearSlot();
                Inv.MyItems.Add(null);
            }
        }
        allAssigned = true;
    }
    void OpenChest()
    {
        GameInfo.CurrentChest = this;
        open = true;
        Prompt.gameObject.SetActive(false);
        animDel = 20;
        for (int i = 0; i <= 8; i++)
        {
            Squares[i].transform.position = transform.position;
            Squares[i].SetActive(true);
        }

    }

    public void CloseChest()
    {
        if (allAssigned)
        {
            delayClose = false;
            allAssigned = false;
            open = false;
            for (int i = 0; i <= 8; i++)
            {
                Squares[i].GetComponent<InvSlot>().ReturnHome();
                Squares[i].SetActive(false);
            }
            allOpen = false;
            transform.root.GetComponent<Animator>().Play("ChestClosed");
            GameInfo.PlayerInMenu = false;
            GameInfo.Player.GetComponent<PlayerInventory>().CloseInv();
            GameInfo.GM.InventoryWindow.SetActive(false);
            GameInfo.ItemInfoWindow.SetActive(false);
            GameInfo.CurrentChest = null;

        }
        else
        {
            delayClose = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !open)
        {
            Prompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !open)
        {
            Prompt.gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Player" && open)
        {
            CloseChest();
        }
    }
}
