using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [Header("Transition")]
    public FadeInOut m_Fader;

    [Header("Loading")]
    public GameObject m_LoadingPanel;
    public float m_DelayAfterLoading = 2.0f;

    public void LoadLevel(string nextSceneName)
    {
        // if(nextSceneName == "") {
        //     Debug.LogError("O nome da cena de destino não foi informado."); 
        //     return;
        // } 
        // else if(!SceneManager.GetSceneByName(nextSceneName).IsValid()) {
        //     Debug.LogError("A cena de destino informada não existe ou não pode ser encontrada."); 
        //     return;
        // }

        StartCoroutine(ChangeScene(nextSceneName, false));
    }

    public void LoadLevelLoading(string nextSceneName)
    {
        StartCoroutine(ChangeScene(nextSceneName, true));
    }

    private IEnumerator ChangeScene(string nextSceneName, bool loading)
    {
        m_Fader.Show();
        yield return new WaitForSeconds(m_Fader.m_Time);

        if (nextSceneName.Equals("Quit"))
        {
            QuitGame();
        }
        else
        {
            AsyncOperation asyncScene = SceneManager.LoadSceneAsync(nextSceneName);
            asyncScene.allowSceneActivation = false;

            while (!asyncScene.isDone)
            {
                if (asyncScene.progress >= 0.9f)
                {               
                    asyncScene.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}