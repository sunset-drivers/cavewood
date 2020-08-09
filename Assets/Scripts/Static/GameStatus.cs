using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cavewood.Models;
using UnityEngine.SceneManagement;

public static class GameStatus
{
    private static List<GameObject> Members;
    private static int Morale;
    private static int Money;
    private static List<string> CompletedCaves;

    public static void SetState(State _State)
    {
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
            Party = _Party,
            CompletedCaves = CompletedCaves
        };
    }

}