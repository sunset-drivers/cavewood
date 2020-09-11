using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Cavewood.Models;

public class GameStatus: MonoBehaviour
{                
    [SerializeField] private State m_State; 
    [SerializeField] private GameObject m_Player;

    private void Awake() {
        m_State = StateManager.Instance.GetState();  
        DontDestroyOnLoad(gameObject);        
    }

    public void Update(){
        if(!m_Player)
            GetPlayer();
    }

    private void GetPlayer(){          
        m_Player = GameObject.Find("Player");
        m_Player.transform.position = m_State.PlayerPosition;
    }
}

