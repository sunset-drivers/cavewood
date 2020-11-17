using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
  //  [SerializeField]
  //  private List<DroppableItem> m_ItemList;
  ////  private int m_MaxQuantity = 3;

  //  public void DropItems()
  //  {
  //      StartCoroutine("iDrop");   
  //  }

  //  IEnumerator iDrop()
  //  {
  //      int max_value = 0;
  //      foreach (DroppableItem item in m_ItemList)
  //      {
  //          max_value += item.drop_rate;             
  //      }
        
  //      int r_Number = (int) Random.Range(0, max_value);
  //      Debug.Log(r_Number);
  //      int top = 0;

  //      foreach (DroppableItem drop in m_ItemList)
  //      {
  //          top += drop.drop_rate;
  //          if(r_Number < top)            
  //              Instantiate(drop.item, transform.position, transform.rotation);break;            
  //      }
        
  //      yield return null;
  //  }

}