using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : SlotInventoryBehaviour
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    [System.Serializable]
    public class InventorySlot{
        public Item currentItem;
        public int m_amount;

        public InventorySlot(Item item, int amount)
        {
            this.currentItem = item;
            this.m_amount = amount;
        }
    }

    public void AddItem(Item item, int amount, Action OnItemAdded)
    {
        foreach(var slot in inventorySlots)
        {
            if (slot.currentItem.nameItem == item.nameItem)
            {
                slot.currentItem.AddItem(amount);
                SetupSlot(item);
                OnItemAdded.Invoke();
                return;
            }
        }

        inventorySlots.Add(new InventorySlot(item, amount));
        SetupSlot(item);
        OnItemAdded.Invoke();
    }
    public void SetupSlot(Item currentItem)
    {
        if (currentItem != null)
        {
            SetActiveSlot(true);
            iconItemSlot.sprite = currentItem.icon;
            nameItem.text = currentItem.nameItem;

            if (currentItem.isStackable)
                amountText.text = currentItem.GetAmount().ToString();
            else
                amountIndicator.SetActive(false);
        }
        else
        {
            SetActiveSlot(false);
        }

    }
}
