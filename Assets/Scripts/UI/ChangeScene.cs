using UnityEngine;
using Cavewood.Models;

public class ChangeScene : MonoBehaviour
{    
    public string m_NextSceneName;    
    public string m_NextSceneSpawnPoint;
    public bool m_UseSpawnPoints = true;    

    private void Awake()
    {        
        if(m_UseSpawnPoints)
            if (string.IsNullOrEmpty(m_NextSceneSpawnPoint))
                Debug.LogError("Necessário especificar a posição de destino do player para a próxima cena");

        if (string.IsNullOrEmpty(m_NextSceneName))
            Debug.LogError("Necessário especificar a cena de destino do player.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            SpawnPoint _NextSpawnPoint = new SpawnPoint(m_NextSceneName, m_NextSceneSpawnPoint);
            GameObject _Player = collision.gameObject;
            State _CurrentState = StateManager.Instance.GetState();
            _CurrentState.SceneName = m_NextSceneName;
            _CurrentState.NextSceneSpawnPoint = _NextSpawnPoint;
            _CurrentState.WasFacingRight = _Player.GetComponent<PlayerPlatformScript>().m_IsFacingRight;
            StateManager.Instance.SetState(_CurrentState);
            LoadLevel(m_NextSceneName);                     
        }
    }

    public void LoadLevel(string sceneName)
    {
        ScreenManager.Instance.LoadLevel(sceneName);
    }

}
