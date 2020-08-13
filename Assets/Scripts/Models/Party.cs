using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cavewood.Models
{    
    [Serializable]
    public class Party
    {
        [SerializeField] public int Morale;
        [SerializeField] public int Money;
        [SerializeField] public List<GameObject> Members;
    }
}