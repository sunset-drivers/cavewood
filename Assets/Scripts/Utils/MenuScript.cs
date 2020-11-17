using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu; 
    public GameObject ajuda; 
    public GameObject creditos; 

    public void NovoJogo(){        
        SceneManager.LoadScene("Level1");
    }

    public void Menu(){
        ajuda.SetActive(false);
        creditos.SetActive(false);
        menu.SetActive(true);
    }

    public void Ajuda(){        
        creditos.SetActive(false);
        menu.SetActive(false);
        ajuda.SetActive(true);
    }

    public void Creditos(){
        ajuda.SetActive(false);
        menu.SetActive(false);
        creditos.SetActive(true);
    }

    public void Sair(){
        Application.Quit();
    }
}
