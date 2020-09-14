using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cavewood.Models;
using Cavewood.Utils;

public class DialogueManager : MonoBehaviour, hasTrigger
{     
    public DialogueBox DialogueBox;
    public float TextSpeed = 1f;
    public List<Dialogue> Dialogues;
    private List<Message> m_Messages;   
    private Message m_ShowingMessage;
    private Dialogue m_ShowingDialogue;
    private bool m_DialogueStarted = false;
    private float m_SpeedUp = 1;
    private int m_Index;

    void Update()
    {
        if(m_DialogueStarted)
        {
            if(DialogueBox.TextBox.text == m_ShowingMessage.Text) 
               DialogueBox.ContinueButton.SetActive(true);            

            if(Input.GetButton("Action"))
                m_SpeedUp = 4;
            else 
                m_SpeedUp = 1;
                
        }    
    }

    public void StartDialogue(string _EventName)
    {
        //Time.timeScale = 0;
        m_DialogueStarted = true;
        DialogueBox.BackgroundPanel.SetActive(true);     
        m_Messages = GetDialogueMessages(_EventName);
        NextMessage(true);   
    }

    public void NextMessage(bool FirstMessage = false)
    {
        m_Index = (FirstMessage) ? 0 : m_Index + 1;                 

        if(m_Index < m_Messages.Count){
            m_ShowingMessage = m_Messages[m_Index];
            SetSpeaker(m_ShowingMessage.IsRightSpeaker);
            DialogueBox.ContinueButton.SetActive(false);
            StartCoroutine("ShowMessage");
        } else {
            ClearDialogue();
        }
    }

    private void SetSpeaker(bool _IsRightSpeaker)
    {
        if(_IsRightSpeaker)
        {
            if(DialogueBox.LeftSpeakerImage.sprite != null)               
                DialogueBox.LeftSpeakerImage.color = new Color32(255,255,255,0);

            DialogueBox.RightSpeakerImage.color = new Color32(255,255,255,255);
            DialogueBox.RightSpeakerImage.sprite = m_ShowingMessage.SpeakerImage;
            DialogueBox.BackgroundPanel.GetComponent<RectTransform>().SetRectLeft(0);
            DialogueBox.BackgroundPanel.GetComponent<RectTransform>().SetRectRight(300);
        } 
        else 
        {
            if(DialogueBox.RightSpeakerImage.sprite != null)                
                DialogueBox.RightSpeakerImage.color = new Color32(255,255,255,0);

            DialogueBox.LeftSpeakerImage.color = new Color32(255,255,255,255);
            DialogueBox.LeftSpeakerImage.sprite = m_ShowingMessage.SpeakerImage;   
            DialogueBox.BackgroundPanel.GetComponent<RectTransform>().SetRectLeft(300);
            DialogueBox.BackgroundPanel.GetComponent<RectTransform>().SetRectRight(0);        
        }
    }

    private void ClearDialogue()
    {
        //Time.timeScale = 1;
        m_DialogueStarted = false;
        DialogueBox.TextBox.text = "";
        DialogueBox.LeftSpeakerImage.sprite = null;
        DialogueBox.RightSpeakerImage.sprite = null;
        DialogueBox.LeftSpeakerImage.color = new Color32(255,255,255,0);
        DialogueBox.RightSpeakerImage.color = new Color32(255,255,255,0);
        m_Index = 0;
        DialogueBox.BackgroundPanel.SetActive(false);
    }

    private List<Message> GetDialogueMessages(string _EventName)
    {
       Dialogue dialogue = Dialogues.Find(x => x.EventName.Contains(_EventName));
       return dialogue.Messages;
    }

    private IEnumerator ShowMessage()
    {
        DialogueBox.TextBox.text = "";
        foreach(char letter in m_ShowingMessage.Text.ToCharArray())
        {
            DialogueBox.TextBox.text += letter;
            yield return new WaitForSeconds(TextSpeed / m_SpeedUp);  
        }
    }

    public void Trigger(string EventName)
    {
        StartDialogue(EventName);
    }
}
