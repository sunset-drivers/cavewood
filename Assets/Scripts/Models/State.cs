using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cavewood.Models
{
    [Serializable]
    public class State
    {
        [SerializeField]
        public string SceneName;
        [SerializeField]
        public Party Party;
        [SerializeField]
        public List<string> CompletedCaves;
    }
}