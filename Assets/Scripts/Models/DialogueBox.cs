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
        [SerializeField] public GameObject BackgroundPanel;
        [SerializeField] public Image LeftSpeakerImage;
        [SerializeField] public Image RightSpeakerImage;
        [SerializeField] public Text TextBox; 
        [SerializeField] public GameObject ContinueButton;
    }
}