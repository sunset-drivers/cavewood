using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cavewood.Models;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGroup> m_EnemyGroups;

    private void Start() {
        int _GroupCount = m_EnemyGroups.Count;
        int _RandomGroup = Random.Range(0, _GroupCount);
        EncounterGroup _SelectedGroup = m_EnemyGroups[_RandomGroup];
        foreach (EnemyInGroup _EnemyInGroup in _SelectedGroup.EnemiesInGroup)
        {
            Instantiate(_EnemyInGroup.Enemy, _EnemyInGroup.Position, _EnemyInGroup.Enemy.transform.rotation);
        }
    }
}
