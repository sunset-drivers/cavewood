using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Cavewood.Models;

public class GameHandler: MonoBehaviour
{                
    [SerializeField] private State m_State; 
    [SerializeField] private GameObject m_Player;

    public void Start()
    {
        m_State = GameStatus.GetState();    
        m_Player = GameObject.Find("Player");
        m_Player.transform.position = m_State.PlayerPosition;
    }

    public void SaveState()
    {
        m_State = GameStatus.GetState();    
        m_State.PlayerPosition = m_Player.transform.position;
        string json = JsonUtility.ToJson(m_State);        
        File.WriteAllText(Application.persistentDataPath + "save.json", json );
    }
}

