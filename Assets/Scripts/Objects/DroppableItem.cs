using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DroppableItem : ScriptableObject
{
    public GameObject item;
    public int drop_rate = 0;

}