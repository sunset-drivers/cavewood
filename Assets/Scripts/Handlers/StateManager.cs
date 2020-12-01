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
    public List<GameObject> m_Members;
    public int m_Morale;
    public int m_Money;
    public List<SlotInventoryBehaviour> m_Inventory;
    public List<string> m_CompletedCaves;

    [Header("Player Info")]
    public SpawnPoint m_NextSceneSpawnPoint;
    public bool m_WasFacingRight;
    public Vector3 m_PlayerPosition;
    public Quaternion m_PlayerRotation;

    private static StateManager _instance;
    public static StateManager Instance {get { return _instance; } }
    private void Awake() {        
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(m_Morale <= 0)
        {
            ScreenManager.Instance.LoadLevel("gameover");
            Destroy(gameObject);
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
        m_Inventory = _State.Inventory;
        m_CompletedCaves = _State.CompletedCaves;
        m_WasFacingRight = _State.WasFacingRight;
        m_NextSceneSpawnPoint = _State.NextSceneSpawnPoint;
    }

    public State GetState()
    {
        Party _Party = new Party(){
            Members = m_Members,
            Money = m_Money,
            Morale = m_Morale
        };
        
        Scene _Scene = SceneManager.GetActiveScene();

        //SpawnPoint _NextSceneSpawnPoint = new SpawnPoint()
        //{
        //    m_Scene = _Scene.name,
        //    m_PointName = m_NextSceneSpawnPoint
        //};

        return new State() {
            SceneName = _Scene.name,
            DateTime = DateTime.Now.ToString(),
            PlayerPosition = m_PlayerPosition,
            Party = _Party,
            Inventory = m_Inventory,
            CompletedCaves = m_CompletedCaves,
            NextSceneSpawnPoint = m_NextSceneSpawnPoint,
            WasFacingRight = m_WasFacingRight
        };
    }

    public void SetMorale(int morale)
    {
        m_Morale = morale;
    }

    public void GainMorale(int gained_morale)
    {
        m_Morale += gained_morale;
    }

    public void LoseMorale(int lost_morale){
        m_Morale -= lost_morale;
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
            PlayerPosition = _Player.transform.position,
            Inventory = _CurrentState.Inventory,
            CompletedCaves = _CurrentState.CompletedCaves            
        };
        SetState(_NewState);
        string json = JsonUtility.ToJson(_NewState);                
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