using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cavewood.Models;

public class StateManager: MonoBehaviour
{    
    [Header("State Info")]
        public string m_ActualSceneName;
        public string m_DateTime;
        public Vector3 m_PlayerPosition;
        public List<GameObject> m_Members;
        public int m_Morale;
        public int m_Money;
        public List<string> m_CompletedCaves;
    private static StateManager _instance;
    public static StateManager Instance {get { return _instance; } }
    private void Awake() {
        Debug.Log("State Started");
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

#region Getters and Setters
    public void SetInitialState(){
        List<GameObject> _Members = new List<GameObject>();    
        //Quando houver o personagem principal, adicionar o .Add() dele no grupo.
        //_Members.Add(Noru);

        Party _Party = new Party() {
            Members = _Members,
            Money = 0,
            Morale = 1000
        };
        
        Scene m_CurrentScene = SceneManager.GetActiveScene();

        List<string> _CompletedCaves = new List<string>();
        State _State = new State() {
            DateTime = DateTime.Now.ToString(),
            SceneName = m_CurrentScene.name,            
            Party = _Party,            
            CompletedCaves = _CompletedCaves
        };

        SetState(_State);
    } 

    public void SetState(State _State)
    {
        m_ActualSceneName = _State.SceneName;
        m_DateTime = DateTime.Now.ToString();
        m_PlayerPosition = _State.PlayerPosition;
        m_Members = _State.Party.Members;
        m_Morale = _State.Party.Morale;
        m_Money = _State.Party.Money;
        m_CompletedCaves = _State.CompletedCaves;
    }

    public State GetState()
    {
        Party _Party = new Party(){
            Members = m_Members,
            Money = m_Money,
            Morale = m_Morale
        };
        
        Scene _Scene = SceneManager.GetActiveScene();

        return new State() {            
            SceneName = _Scene.name,            
            DateTime = DateTime.Now.ToString(),
            PlayerPosition = m_PlayerPosition,
            Party = _Party,
            CompletedCaves = m_CompletedCaves
        };
    }    
#endregion
#region State Functions
    public void SaveState()
    {
        GameObject _Player = GameObject.FindGameObjectWithTag("Player");                   
        State _CurrentState = GetState();
        State _NewState = new State(){
            SceneName = SceneManager.GetActiveScene().name,
            DateTime = DateTime.Now.ToString(),
            Party = _CurrentState.Party,
            CompletedCaves = _CurrentState.CompletedCaves,
            PlayerPosition = _Player.transform.position
        };
        SetState(_NewState);
        string json = JsonUtility.ToJson(_NewState);        
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "save.json", json );
    }   

    public void LoadState()
    {
        Debug.Log(Application.persistentDataPath + "save.json");
        State _State = JsonUtility.FromJson<State>(File.ReadAllText(Application.persistentDataPath + "save.json"));
        StateManager.Instance.SetState(_State);
        SceneManager.LoadScene(_State.SceneName);
    }
#endregion
}