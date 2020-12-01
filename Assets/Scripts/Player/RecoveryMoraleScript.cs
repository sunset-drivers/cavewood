using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryMoraleScript : MonoBehaviour
{
    public StateManager m_StateManager;
    private void Start()
    {
        m_StateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))                    
            StartCoroutine("Recover");        
    }

    IEnumerator Recover()
    {
        do
        {
            m_StateManager.GainMorale(50);
            yield return null;
        } while (m_StateManager.m_Morale < 1000);
        m_StateManager.SetMorale(1000);
    }
}
