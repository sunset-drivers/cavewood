using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Cavewood.Models;

public class MainMenuHandler : MonoBehaviour {
    
    public void NewGame()
    {
        List<GameObject> _Members = new List<GameObject>();    
        //Quando houver o personagem principal, adicionar o .Add() dele no grupo.
        //_Members.Add(Noru);

        Party _Party = new Party() {
            Members = _Members,
            Money = 0,
            Morale = 1000
        };
        
        List<string> _CompletedCaves = new List<string>();
        State _State = new State() {
            Party = _Party,            
            CompletedCaves = _CompletedCaves
        };

        GameStatus.SetState(_State);
        SceneManager.LoadScene("worldmap_player");
    }

    public void LoadGame()
    {
        State _State = JsonUtility.FromJson<State>(File.ReadAllText(Application.persistentDataPath + "save.json"));
        GameStatus.SetState(_State);
        SceneManager.LoadScene(_State.SceneName);
    }

}