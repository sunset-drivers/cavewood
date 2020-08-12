using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cavewood.Models;

public class DialogManager : MonoBehaviour
{     
    public DialogueBox DialogueBox;
    public List<Dialogue> Dialogues;
    private List<Message> Messages;   
    private Message _ShowingMessage;
    private bool m_DialogStarted = false;
    private int m_Index;

    void Update()
    {
        if(m_DialogStarted){
            if(Input.GetButtonDown("Jump")) {
                NextMessage();   
            }
        }    
    }

    public void StartDialog(){
        m_DialogStarted = true;
        DialogueBox.DialogBox.SetActive(true);     
        NextMessage(true);   
    }

    private void NextMessage(bool FirstMessage = false)
    {
        m_Index = (FirstMessage) ? 0 : m_Index + 1;                 

        if(m_Index < Messages.Count){
            _ShowingMessage = Messages[m_Index];
            SetSpeaker(_ShowingMessage.IsLeftSpeaker);
            DialogueBox.TextBox.text = _ShowingMessage.Text;
        } else {
            ClearDialog();
        }
    }

    private void SetSpeaker(bool _IsLeftSpeaker){
        if(_IsLeftSpeaker){

            if(DialogueBox.SpeakerB.sprite != null)                
                DialogueBox.SpeakerB.color = new Color32(50,50,50,150);

            DialogueBox.SpeakerA.color = new Color32(255,255,255,255);
            DialogueBox.SpeakerA.sprite = _ShowingMessage.SpeakerImage;
        } else {

            if(DialogueBox.SpeakerA.sprite != null)               
                DialogueBox.SpeakerA.color = new Color32(50,50,50,150);

            DialogueBox.SpeakerB.color = new Color32(255,255,255,255);
            DialogueBox.SpeakerB.sprite = _ShowingMessage.SpeakerImage;
        }
    }

    private void ClearDialog(){
        m_DialogStarted = false;
        DialogueBox.TextBox.text = "";
        DialogueBox.SpeakerA.sprite = null;
        DialogueBox.SpeakerB.sprite = null;
        DialogueBox.SpeakerA.color = new Color32(255,255,255,0);
        DialogueBox.SpeakerB.color = new Color32(255,255,255,0);
        m_Index = 0;
        DialogueBox.DialogBox.SetActive(false);
    }
}
