using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cavewood.Models
{
    [Serializable]
    public class Message
    {
        [SerializeField] public string Text;
        [SerializeField] public string SpeakerName;
        [SerializeField] public Sprite SpeakerImage;    
        [SerializeField] public bool IsLeftSpeaker;         
    }
}