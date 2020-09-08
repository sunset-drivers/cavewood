using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldMapScript : MonoBehaviour
{    
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Animator Variables")]
    public string m_AnimatorXAxis = "xAxis";
    public string m_AnimatorYAxis = "yAxis";

    public float m_Speed = 5f;
    private float m_xAxis;
    private float m_yAxis;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_xAxis = Input.GetAxisRaw("Horizontal");
        m_yAxis = Input.GetAxisRaw("Vertical");

        animator.SetFloat(m_AnimatorXAxis, m_xAxis);
        animator.SetFloat(m_AnimatorYAxis, m_yAxis);

        if(Input.GetButtonDown("Inventory"))
            InventoryController.Instance.OpenInventory();         
    }

    private void FixedUpdate() 
    {        
        float _currentSpeed = m_Speed;
        if(m_xAxis != 0.0f && m_yAxis != 0.0f)
            _currentSpeed = m_Speed / 1.414f;

        rb.velocity = new Vector2(m_xAxis, m_yAxis) * _currentSpeed;
    }
}
