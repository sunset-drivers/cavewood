using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Cavewood.Models;

public class MainMenuHandler : MonoBehaviour {
    
    public void NewGame()
    {        
        SceneManager.LoadScene("worldmap_player");
    }    

}