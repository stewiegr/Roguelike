using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro Prompt;
    public SpriteRenderer Item1;
    public SpriteRenderer Item2;
    bool open = false;

    Vector2 Item1Pos;
    Vector2 Item2Pos;

    void Start()
    {
        Item1Pos = Item1.transform.position;
        Item2Pos = Item2.transform.position;
        Item1.transform.position = transform.position;
        Item2.transform.position = transform.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Prompt.gameObject.activeSelf)
            {
                OpenChest();
            }
        }

        if(open)
        {
            LerpItemsOut();
        }
        if(!open && Item1.gameObject.activeSelf)
        {
            if (Item1.transform.position.y > transform.position.y)
                Item1.transform.position = Vector2.Lerp(Item1.transform.position, transform.position, 6 * Time.deltaTime);
            else
                Item1.transform.position = transform.position;
            if (Item2.transform.position.y > transform.position.y)
                Item2.transform.position = Vector2.Lerp(Item2.transform.position, transform.position, 6 * Time.deltaTime);
            else
                Item2.transform.position = transform.position;

            if(Vector2.Distance(Item1.transform.position, transform.position) < .2f)
            {
                Item1.gameObject.SetActive(false);
                Item2.gameObject.SetActive(false);
            }
        }


    }
    void LerpItemsIn()
    {

    }
    void LerpItemsOut()
    {
        if (Item1.transform.position.y < Item1Pos.y)
            Item1.transform.position = Vector2.Lerp(Item1.transform.position, Item1Pos, 6 * Time.deltaTime);
        else
            Item1.transform.position = Item1Pos;
        if (Item2.transform.position.y < Item2Pos.y)
            Item2.transform.position = Vector2.Lerp(Item2.transform.position, Item2Pos, 6 * Time.deltaTime);
        else
            Item2.transform.position = Item2Pos;
    }
    void OpenChest()
    {
        Item1.gameObject.SetActive(true);
        Item2.gameObject.SetActive(true);
        open = true;
        Prompt.gameObject.SetActive(false);
    }

    void CloseChest()
    {
        open = false;
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
