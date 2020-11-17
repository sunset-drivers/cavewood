using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cavewood.Models;
using Cinemachine;

public class PlayerPlatformSpawner : MonoBehaviour
{
    public GameObject m_PlayerPrefab;
    public CinemachineVirtualCamera m_CMCamera;

    private void Start()
    {
        bool _PlayerExist = (GameObject.Find("Player") != null);

        if(m_CMCamera == null)
        {
            GameObject _CameraSideScroll = GameObject.Find("CameraSidescroll");
            m_CMCamera = _CameraSideScroll.transform.Find("CMCamera").GetComponent<CinemachineVirtualCamera>();
        }

        if (!_PlayerExist)
        {
            State _CurrentState = StateManager.Instance.GetState();
            SpawnPoint _CurrentSpawnPoint;

            if(_CurrentState.NextSceneSpawnPoint != null)
                _CurrentSpawnPoint = _CurrentState.NextSceneSpawnPoint;
            else
                _CurrentSpawnPoint = null;            
            
            Vector3 _SpawnAt = (_CurrentSpawnPoint != null) 
            ? transform.Find(_CurrentSpawnPoint.m_PointName).position
            : transform.Find("A").position;

            GameObject _player = Instantiate(m_PlayerPrefab, _SpawnAt, m_PlayerPrefab.transform.rotation);
            m_CMCamera.Follow = _player.transform;

            if (!StateManager.Instance.GetState().WasFacingRight)
                _player.GetComponent<PlayerPlatformScript>().ChangeDirection();
        }        
    }

    
}
