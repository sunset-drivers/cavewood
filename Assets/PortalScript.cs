using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalScript : MonoBehaviour
{
    private ChangeScene m_SceneChanger;    
    private void Awake(){
        m_SceneChanger = GetComponent<ChangeScene>();       
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))    
            m_SceneChanger.LoadLevel(m_SceneChanger.m_SceneName);
    }
}
