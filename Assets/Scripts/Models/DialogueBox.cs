using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cavewood.Models
{
    [Serializable]
    public class DialogueBox
    {
        [SerializeField] public GameObject DialogBox;
        [SerializeField] public Image SpeakerA;
        [SerializeField] public Image SpeakerB;
        [SerializeField] public Text TextBox; 
    }
}