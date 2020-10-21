using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Morale Info")]
    public GameObject m_MoraleBackground;
    public Text m_MoraleText;

    [Header("Money Info")]
    public GameObject m_MoneyBackground;    
    public Text m_MoneyText;
    
    void Awake()
    {    
        m_MoneyBackground = transform.Find("bgMoney").gameObject;
        m_MoneyText = m_MoneyBackground.transform.Find("txtMoney").GetComponent<Text>();

        m_MoraleBackground = transform.Find("bgMorale").gameObject;
        m_MoraleText = m_MoraleBackground.transform.Find("txtMorale").GetComponent<Text>();
    }
    
    void Update()
    {
        m_MoraleText.text = StateManager.Instance.m_Morale.ToString();  
        m_MoneyText.text = StateManager.Instance.m_Money.ToString();  
    }
}
