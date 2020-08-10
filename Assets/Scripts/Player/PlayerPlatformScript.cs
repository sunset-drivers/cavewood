using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    [Header("Sensors")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool m_IsGrounded = false;

    [Header("Movement Variables")]
    public float m_Speed = 5f;
    public float m_JumpForce = 10f;

    [Header("Animator Variables")]
    public string m_AnimatorAxisXName;
    public string m_AnimatorAxisYName;

    private float m_AxisX;
    private float m_AxisY;

    [Header("State Variables")]        
    public bool m_IsFacingRight = true;

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    void FixedUpdate() {
        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        m_AxisX = Input.GetAxis("Horizontal");
        m_AxisY = rb.velocity.y;       
        Debug.Log(m_AxisY);

        animator.SetFloat(m_AnimatorAxisXName, m_AxisX);        
        animator.SetFloat(m_AnimatorAxisYName, m_AxisY);

        if( m_AxisX > 0.2f && !m_IsFacingRight || m_AxisX < -0.2f && m_IsFacingRight)
            Flip();                                                        

        animator.SetFloat(m_AnimatorAxisXName, m_AxisX);        
        rb.velocity = new Vector2(m_AxisX * m_Speed, rb.velocity.y);                                     
    }

    void Update(){
        if(m_IsGrounded && Input.GetButtonDown("Jump"))
           rb.velocity = Vector2.up * m_JumpForce;
    }

    private void Flip(){
        m_IsFacingRight = !m_IsFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
