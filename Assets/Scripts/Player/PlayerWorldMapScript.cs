using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldMapScript : MonoBehaviour
{    
    private Animator animator;
    private Rigidbody2D rigidbody;

    [Header("Animator Variables")]
    public string m_AnimatorXAxis = "xAxis";
    public string m_AnimatorYAxis = "yAxis";

    public float m_speed = 5f;
    private float m_xAxis;
    private float m_yAxis;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_xAxis = Input.GetAxisRaw("Horizontal");
        m_yAxis = Input.GetAxisRaw("Vertical");

        animator.SetFloat(m_AnimatorXAxis, m_xAxis);
        animator.SetFloat(m_AnimatorYAxis, m_yAxis);                
    }

    private void FixedUpdate() 
    {
        rigidbody.velocity = new Vector2(m_xAxis, m_yAxis) * m_speed;
    }
}
