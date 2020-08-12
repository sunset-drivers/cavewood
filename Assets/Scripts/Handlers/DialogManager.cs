using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cavewood.Models;

public class DialogManager : MonoBehaviour
{
    public List<Message> Messages;        
    public Image SpeakerA;
    public Image SpeakerB;
    public Text TextBox;
    public GameObject DialogBox;
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
        DialogBox.SetActive(true);     
        NextMessage(true);   
    }

    private void NextMessage(bool FirstMessage = false)
    {
        m_Index = (FirstMessage) ? 0 : m_Index + 1;                 

        if(m_Index < Messages.Count){
            _ShowingMessage = Messages[m_Index];
            SetSpeaker(_ShowingMessage.IsLeftSpeaker);
            TextBox.text = _ShowingMessage.Text;
        } else {
            ClearDialog();
        }
    }

    private void SetSpeaker(bool _IsLeftSpeaker){
        if(_IsLeftSpeaker){

            if(SpeakerB.sprite != null)                
                SpeakerB.color = new Color32(50,50,50,150);

            SpeakerA.color = new Color32(255,255,255,255);
            SpeakerA.sprite = _ShowingMessage.SpeakerImage;
        } else {

            if(SpeakerA.sprite != null)               
                SpeakerA.color = new Color32(50,50,50,150);

            SpeakerB.color = new Color32(255,255,255,255);
            SpeakerB.sprite = _ShowingMessage.SpeakerImage;
        }
    }

    private void ClearDialog(){
        m_DialogStarted = false;
        TextBox.text = "";
        SpeakerA.sprite = null;
        SpeakerB.sprite = null;
        SpeakerA.color = new Color32(255,255,255,0);
        SpeakerB.color = new Color32(255,255,255,0);
        m_Index = 0;
        DialogBox.SetActive(false);
    }
}
