using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<SlotInventoryBehaviour> inventorySlots;    
    public int maxInventorySlots;
    public SlotInventoryBehaviour slotPrefab;  

    [Header("UI Components")]  
        public GameObject m_Inventory;
        private GameObject m_BGFader;
        private GameObject m_ItemGrid;

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
        m_Inventory.SetActive(!m_Inventory.gameObject.activeInHierarchy);
        Time.timeScale = m_Inventory.activeInHierarchy ? 0 : 1;
    }
#endregion
#region Manager Functions
    public void LoadUIComponents(){
        m_Inventory = GameObject.Find("Inventory");

        Transform _ItemGridTransform = m_Inventory.gameObject.transform.Find("ItemGrid");
        m_ItemGrid = _ItemGridTransform.gameObject;        

        Transform _BGFaderTransform = m_Inventory.gameObject.transform.Find("bgFader");
        m_BGFader = _BGFaderTransform.gameObject;                
        
        m_Inventory.SetActive(false);
    }
#endregion
}
