using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapSpot : MonoBehaviour
{
    public string SpotName;      
    public GameObject SpotBackground;
    public Text SpotText;   
    private ChangeScene m_ChangeScene;
    private bool m_CanChangeScene = false;

    private void Start()
    {
        m_ChangeScene = GetComponent<ChangeScene>();
    }

    void Update()
    {        
        if(m_CanChangeScene && Input.GetButtonDown("Fire1"))
            m_ChangeScene.LoadLevel(m_ChangeScene.m_SceneName);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            SpotText.text = SpotName;
            SpotBackground.SetActive(true);            
            m_CanChangeScene = true;                            
        }
    }           

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SpotText.text = "";
            SpotBackground.SetActive(false);                        
            m_CanChangeScene = false;
        }     
    }        
}
