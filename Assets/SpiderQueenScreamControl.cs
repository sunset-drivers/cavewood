using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderQueenScreamControl : MonoBehaviour
{
    public GameObject m_SpiderPrefab;
    public List<Transform> m_Spawners;
    public float m_SpawnCountDown = 15f;
    public Animator m_HeadAnimator;    
    private float _Counter = 0.0f;
    
    void Update()
    {
        if(_Counter < m_SpawnCountDown)
        {
            _Counter += Time.deltaTime;
        }
        else
        {
            SpawnChild();
            _Counter = 0.0f;
        }
    }

    private void SpawnChild()
    {
        m_HeadAnimator.SetTrigger("Scream");
        foreach(Transform _spawner in m_Spawners)
        {
            Instantiate(m_SpiderPrefab, _spawner.position, _spawner.rotation);
        }
    }
}
