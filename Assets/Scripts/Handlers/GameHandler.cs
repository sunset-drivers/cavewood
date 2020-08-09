using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Cavewood.Models;

public class GameHandler: MonoBehaviour
{                
    [SerializeField]
    private State m_State; 

    public void Start()
    {
        m_State = GameStatus.GetState();                
    }

    public void SaveState()
    {
        m_State = GameStatus.GetState();    
        string json = JsonUtility.ToJson(m_State);        
        File.WriteAllText(Application.dataPath + "/Files/save.json", json );
    }
}

