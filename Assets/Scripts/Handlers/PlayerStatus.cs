using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("Morale Info")]
    public Sprite m_FullHeartSprite;
    public Sprite m_HalfHeartSprite;
    public Sprite m_EmptyHeartSprite;
    public GameObject[] m_MoraleContainers;
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

        m_MoraleContainers = GameObject.FindGameObjectsWithTag("MoraleContainer");
    }
    
    void Update()
    {
        if(StateManager.Instance == null) {
            Debug.LogError("O GameManager não foi encontrado, adicione-o a cena.");
            return;
        } else {
            FillHearts();
            //m_MoraleText.text = StateManager.Instance.m_Morale.ToString();  
            //m_MoneyText.text = StateManager.Instance.m_Money.ToString();  
        }                    
    }

    private void FillHearts()
    {
        float m_Morale = StateManager.Instance.m_Morale/100f;
        foreach(GameObject container in m_MoraleContainers)
        {
            if (m_Morale <= 0.0f)
                container.GetComponent<Image>().sprite = m_EmptyHeartSprite;
            else if (m_Morale > 0.0f && m_Morale < 1.0f)
                container.GetComponent<Image>().sprite = m_HalfHeartSprite;
            else
                container.GetComponent<Image>().sprite = m_FullHeartSprite;

            m_Morale -= 1;
        }
    }
}
