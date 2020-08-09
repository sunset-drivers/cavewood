using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cavewood.Models;
using UnityEngine.SceneManagement;

public static class GameStatus
{
    public static Vector3 PlayerPosition;
    public static List<GameObject> Members;
    public static int Morale;
    public static int Money;
    public static List<string> CompletedCaves;

    public static void SetState(State _State)
    {
        PlayerPosition = _State.PlayerPosition;
        Members = _State.Party.Members;
        Morale = _State.Party.Morale;
        Money = _State.Party.Money;
        CompletedCaves = _State.CompletedCaves;
    }

    public static State GetState()
    {
        Party _Party = new Party(){
            Members = Members,
            Money = Money,
            Morale = Morale
        };
        
        Scene _Scene = SceneManager.GetActiveScene();

        return new State() {            
            SceneName = _Scene.name,            
            PlayerPosition = PlayerPosition,
            Party = _Party,
            CompletedCaves = CompletedCaves
        };
    }

}