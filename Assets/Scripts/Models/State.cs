using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cavewood.Models
{
    [Serializable]
    public class State
    {
        [SerializeField] public string DateTime;
        [SerializeField] public string SceneName;
        [SerializeField] public Vector3 PlayerPosition;
        [SerializeField] public Party Party;
        [SerializeField] public List<SlotInventoryBehaviour> Inventory;
        [SerializeField] public List<string> CompletedCaves;        
        [SerializeField] public bool WasFacingRight;
        [SerializeField] public SpawnPoint NextSceneSpawnPoint;
        [SerializeField] public SpawnPoint LastSceneSpawnPoint;
    }
}