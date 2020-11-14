using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cavewood.Models;

public class EncounterManager : MonoBehaviour
{
    public List<EncounterGroup> m_EnemyGroups;
    private ChangeScene m_ChangeScene;
    private Text TimerText;
    public GameObject TimerCanvas;
    public bool m_FinishedEncounter = false;
    private void Awake() {
        m_ChangeScene = GetComponent<ChangeScene>();
        TimerText = TimerCanvas.GetComponentInChildren<Text>();
    }

    private void Start() {
        int _GroupCount = m_EnemyGroups.Count;
        int _RandomGroup = Random.Range(0, _GroupCount);
        EncounterGroup _SelectedGroup = m_EnemyGroups[_RandomGroup];
        foreach (EnemyInGroup _EnemyInGroup in _SelectedGroup.EnemiesInGroup)
        {
            Instantiate(_EnemyInGroup.Enemy, _EnemyInGroup.Position, _EnemyInGroup.Enemy.transform.rotation);
        }
    }
    
    private void Update(){
        GameObject[] _Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(_Enemies.Length == 0 && !m_FinishedEncounter)
        {
            StartCoroutine("StartChangeSceneTimer");
        }
    }

    public IEnumerator StartChangeSceneTimer(){
        m_FinishedEncounter = true;
        TimerCanvas.SetActive(true);
        float _PastSeconds = 5.0f;
        do {
            _PastSeconds -= Time.deltaTime;
            if(_PastSeconds < 0.0f) _PastSeconds = 0.0f;
            TimerText.text = Mathf.RoundToInt(_PastSeconds).ToString();
            yield return null;
        } while(_PastSeconds > 0.0f);
    
        TimerCanvas.SetActive(false);
        m_ChangeScene.LoadLevel(m_ChangeScene.m_NextSceneName);
    }
}
