using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLineController : MonoBehaviour
{
    public float m_LineSpeed = 1.0f;
    public float m_MaxHeight;
    void Update()
    {
        if (transform.position.y < m_MaxHeight)
            transform.Translate(Vector3.up * Time.deltaTime * m_LineSpeed);
        else
            Destroy(gameObject);
    }
}
