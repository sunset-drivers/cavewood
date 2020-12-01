using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TextShowerManager : MonoBehaviour
{
    public TriggerEvent m_TriggerEvent;
    public string m_TriggerEventName;

    public GameObject m_TextLinePrefab;
    public Transform m_TextCanvas;
    public List<string> m_Lines;
    public Transform m_UpperBound;
    public Transform m_LowerBound;
    public int m_OriginalLinesCount;
    public float m_LineSpeed = 1.0f;
    public float m_LineSpawnRate = 100.0f;
    private float m_Counter = 0.0f;

    void Start()
    {
        m_UpperBound = GameObject.Find("UpperBound").transform;
        m_LowerBound = GameObject.Find("LowerBound").transform;
        m_TextCanvas = GameObject.Find("TextCanvas").transform;
        m_OriginalLinesCount = m_Lines.Count;
    }
    
    void Update()
    {   
        if(m_Lines.Count > 0)
        {
            if (m_Counter < m_LineSpawnRate && m_OriginalLinesCount != m_Lines.Count)
            {;            
                m_Counter += Time.deltaTime;
            }
            else
            {
                m_Counter = 0.0f;
                SpawnTextLine(m_Lines[0]);
                m_Lines.Remove(m_Lines[0]);
            }
        }
        else
        {
            GameObject[] _SpawnedLines = GameObject.FindGameObjectsWithTag("TextLine");
            if (_SpawnedLines.Length <= 0)
            {
                if (m_TriggerEvent)
                {
                    m_TriggerEvent.Trigger(m_TriggerEventName);
                    m_TriggerEvent = null;
                }
            }                
        }        
    }

    private void SpawnTextLine(string Line)
    {
        GameObject TextLine = Instantiate(m_TextLinePrefab, m_LowerBound.position, m_LowerBound.rotation, m_TextCanvas);
        TextLine.GetComponent<Text>().text = Line;
        TextLine.GetComponent<TextLineController>().m_LineSpeed = m_LineSpeed;
        TextLine.GetComponent<TextLineController>().m_MaxHeight = m_UpperBound.position.y;
    }
}
