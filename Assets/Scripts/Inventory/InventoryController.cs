using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<SlotInventoryBehaviour> inventorySlots;
    public static InventoryController instance;
    public int maxInventorySlots;
    public SlotInventoryBehaviour slotPrefab;
    public Transform itemsGrid;

    void Start()
    {
        instance = this;   
        
        for(int i=0; i<maxInventorySlots; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(itemsGrid, false);
            inventorySlots.Add(tempSlot.GetComponent<SlotInventoryBehaviour>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemToInventory(Item item)
    {
        bool foundItem = false;
        SlotInventoryBehaviour emptySlot = nextEmptySlot();
        if (item.isStackable)
        {
            foreach(SlotInventoryBehaviour slot in inventorySlots)
            {
                if(slot.currentItem != null && slot.currentItem.nameItem == item.nameItem)
                {
                    slot.currentItem.AddItem();
                    foundItem = true;
                }
            }

            if (!foundItem && emptySlot != null) 
            {
                emptySlot.currentItem = item;
            }
        }
        else if(emptySlot != null)
        {
            emptySlot.currentItem = item;
        }
        item.gameObject.SetActive(false);
    }
    

    private SlotInventoryBehaviour nextEmptySlot()
    {
        SlotInventoryBehaviour slotToReturn = null;
        foreach(SlotInventoryBehaviour slot in inventorySlots)
        {
            if (slot.currentItem == null)
            {
                slotToReturn = slot;
                break;
            }
        }
        return slotToReturn;
    }


}
