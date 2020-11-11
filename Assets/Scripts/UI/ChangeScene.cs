using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public string m_SceneName;
    public bool m_Used;

    public void LoadLevel(string sceneName)
    {
        if (m_Used)
            return;

        m_Used = true;
        ScreenManager.Instance.LoadLevel(sceneName);
    }

    public void LoadLevelWithLoading(string sceneName)
    {
        if (m_Used)
            return;
       
        m_Used = true;
        ScreenManager.Instance.LoadLevelLoading(sceneName);
    }
}
