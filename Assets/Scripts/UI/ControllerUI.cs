using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUI : MonoBehaviour
{
    public InventoryController inventory;
    public float defaultTimeScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        inventory.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);

        Time.timeScale = inventory.gameObject.activeInHierarchy ? 0 : defaultTimeScale;
    }
}
