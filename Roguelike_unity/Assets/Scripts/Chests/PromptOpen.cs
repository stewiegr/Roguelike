using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro Prompt;
    bool open = false;
    float animDel = 20;
    public List<GameObject> Squares;
    public List<Vector2> SquareCoords = new List<Vector2>();
    bool allOpen = false;

    ChestInventory Inv;

    void Start()
    {
        for (int i = 0; i <= 7; i++)
            SquareCoords.Add((Vector2)Squares[i].transform.position);

        Inv = transform.root.GetComponent<ChestInventory>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Prompt.gameObject.activeSelf)
            {
                transform.root.GetComponent<Animator>().Play("WoodChestOpen");
                OpenChest();
            }
        }

        if(open)
        {
            if (animDel <= 0)
            {
                if(!allOpen)
                {
                    for (int i = 0; i <= 8; i++)
                        Squares[i].SetActive(true);
                    allOpen = true;

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
            }
        }


    }
    void AssignItems()
    {
        int index = 0;
        foreach(Item item in Inv.MyItems)
        {
            Squares[index].GetComponent<InvSlot>().UpdateSlot(item);
            index++;
        }
        if(index<9)
        {
            for(int i = index; i<9; i++)
            {
                Squares[index].GetComponent<InvSlot>().ClearSlot();              
            }
        }
    }
    void OpenChest()
    {
        open = true;
        Prompt.gameObject.SetActive(false);
        animDel = 20;

    }

    void CloseChest()
    {
        open = false;
        for (int i = 0; i <= 8; i++)
            Squares[i].SetActive(false);
        allOpen = false;
        transform.root.GetComponent<Animator>().Play("WoodChestClosed");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Player" && !open)
        {
            Prompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Prompt.gameObject.SetActive(false);
        }
        if (collision.transform.tag == "Player" && open)
        {
            CloseChest();
        }
    }
}
