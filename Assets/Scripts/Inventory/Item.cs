using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string nameItem;
    public Sprite icon;
    public bool isStackable;
    protected int amount = 1;
    private bool canTakeItem;
    public void AddItem(int amountToAdd = 1)
    {
        amount += amountToAdd;
    }
    public int GetAmount()
    {
        return amount;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InventoryController.instance.AddItemToInventory(this);
        }
    }

}
