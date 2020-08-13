using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cavewood.Models
{
    [Serializable]
    public class Message
    {

        [SerializeField] [TextArea] [Header("Max. 120 Characters")] public string Text;
        [SerializeField] public string SpeakerName;
        [SerializeField] public Sprite SpeakerImage;    
        [SerializeField] public bool IsRightSpeaker;         
    }
}