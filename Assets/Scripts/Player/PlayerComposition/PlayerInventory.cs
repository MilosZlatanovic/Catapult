using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Testing Inventory
    public InventoryObject_SO inventory;
    DisplayInventory display;

    private void Start()
    {
        inventory.Load();
        /* // For Testing
         display = GameObject.FindWithTag("DisplayInventory").GetComponent<DisplayInventory>();
         inventory.Load();
         if (display == null)
         {
             Debug.Log("display is NULL");
             //return;
         }*/
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Debug.Log(item + "Is ADD");
            Destroy(other.gameObject);
        }
        //Testing
        // display.UpdateDisplay();
    }
    private void OnApplicationQuit()
    {
        inventory.Save();
        inventory.Container.Clear();
    }
}
