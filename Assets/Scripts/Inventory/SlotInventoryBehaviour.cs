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
  
    public void SetActiveSlot(bool isActive = true)
    {
        amountIndicator.SetActive(isActive);
        nameItem.gameObject.SetActive(isActive);
        iconItemSlot.gameObject.SetActive(isActive);
    }

    private void OnEnable()
    {
        //SetupSlot();  
    }
}
