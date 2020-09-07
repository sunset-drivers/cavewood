using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cavewood.Models
{
    [Serializable]
    public class EnemyInGroup
    {
        [SerializeField] public GameObject Enemy;
        [SerializeField] public Vector3 Position;
    }
    
    [Serializable]
    public class EncounterGroup 
    {
        [SerializeField] public string GroupName;
        [SerializeField] public List<EnemyInGroup> EnemiesInGroup;
    }

}