using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventoryBehaviour : MonoBehaviour
{
    public Item currentItem;
    public Image iconItemSlot;
    public Text nameItem;
    public GameObject amountIndicator;
    public Text amountText;
    
    void Start()
    {
        SetupSlot();
    }

    public void SetupSlot()
    {
        if(currentItem != null)
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

    public void SetActiveSlot(bool isActive = true)
    {
        amountIndicator.SetActive(isActive);
        nameItem.gameObject.SetActive(isActive);
        iconItemSlot.gameObject.SetActive(isActive);
    }

    private void OnEnable()
    {
        SetupSlot();  
    }
}
