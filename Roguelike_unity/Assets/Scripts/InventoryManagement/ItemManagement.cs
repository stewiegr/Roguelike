using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{
    public InvSlot ParentSlot;
    public InvSlot CurrentDestination;

    private void Start()
    {
        ParentSlot = transform.parent.GetComponent<InvSlot>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag=="ItemSlot")
        {
            CurrentDestination = collision.transform.GetComponent<InvSlot>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "ItemSlot")
        {
            CurrentDestination = ParentSlot;
        }
    }

    public void CancelInteract()
    {
        CurrentDestination = ParentSlot;
        DropItem();
    }

    public InvSlot DropItem()
    {
        if (CurrentDestination != null)
            return CurrentDestination;
        else
            return ParentSlot;
    }
}
