using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public List<SlotInventoryBehaviour> inventorySlots;    
    public int maxInventorySlots;
    public SlotInventoryBehaviour slotPrefab;  

    [Header("UI Components")]  
        public GameObject m_Inventory;
        public GameObject m_BGFader;
        public GameObject m_ItemGrid;

    private static InventoryController _instance;
    public static InventoryController Instance {get { return _instance; } }

#region Behaviours
    void Awake()
    {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }         

        LoadUIComponents();

        for(int i=0; i<maxInventorySlots; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(m_ItemGrid.transform, false);
            inventorySlots.Add(tempSlot.GetComponent<SlotInventoryBehaviour>());
        }
    }

    void Update(){
        if(Input.GetButtonDown("Inventory"))
            InventoryController.Instance.OpenInventory();  

        // if(!m_Inventory)
        //     LoadUIComponents();
    }
#endregion
#region Inventory Functions
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

    public void OpenInventory() {                
        // Time.timeScale = m_Inventory.activeInHierarchy ? 0 : 1;
        m_BGFader.SetActive(!m_BGFader.activeInHierarchy);
        m_ItemGrid.SetActive(!m_ItemGrid.activeInHierarchy);
    }
#endregion
#region Manager Functions
    public void LoadUIComponents(){
        
    }
#endregion
}
