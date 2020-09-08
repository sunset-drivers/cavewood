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
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        m_State = StateManager.Instance.GetState();    
        m_Player = GameObject.Find("Player");
        m_Player.transform.position = m_State.PlayerPosition;
    }    
}

