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

        Inv = transform.parent.GetComponent<ChestInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameInfo.CurrentChest==this && !open)
            {
                GameInfo.PlayAudio(4);
                transform.parent.GetComponent<Animator>().Play("ChestOpen");
                OpenChest();
            }
            else if(GameInfo.CurrentChest==this)
            {
                //GameInfo.GM.ActivateInvWindow(false);
                CloseChest();
                Prompt.gameObject.SetActive(true);
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
                    // GameInfo.GM.ActivateInvWindow();
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
        GameInfo.GM.ActivateInvWindow(true);
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
            transform.parent.GetComponent<Animator>().Play("ChestClosed");
            GameInfo.GM.ActivateInvWindow(false);

        }
        else
        {
            delayClose = true;
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !open)
        {
            if (GameInfo.CurrentChest == null)
                GameInfo.CurrentChest = this;
            Prompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !open)
        {
            if (GameInfo.CurrentChest == this)
                GameInfo.CurrentChest = null;
            Prompt.gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Player" && open)
        {
            if (GameInfo.CurrentChest == this)
                GameInfo.CurrentChest = null;
            CloseChest();
        }
    }
}
