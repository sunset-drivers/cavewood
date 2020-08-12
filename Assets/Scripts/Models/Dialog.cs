using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cavewood.Models
{
    [Serializable]
    public class Dialogue
    {        
        [SerializeField] public string EventName;
        [SerializeField] public List<Message> Messages;
    }    
}