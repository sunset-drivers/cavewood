using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformScript : MonoBehaviour
{
    [Header("Components")]
        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer sprite;

    [Header("Sensors")]
        public Transform groundCheck;
        public float checkRadius;
        public LayerMask whatIsGround;
        public GameObject hit;
        public bool m_IsGrounded = false;

    [Header("Movement Variables")]
        public float m_Speed = 5f;
        public float m_JumpForce = 10f;

    [Header("Animator Variables")]
        public string m_AnimatorAxisXName;
        public string m_AnimatorAxisYName;

    [Header("State Variables")]        
        public bool m_IsFacingRight = true; 

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    void FixedUpdate() {
        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(Input.GetButton("Action"))
            hit.SetActive(true);

        float _AxisX = Input.GetAxis("Horizontal");
        float _AxisY = rb.velocity.y;               

        if(m_AnimatorAxisXName != "")
            animator.SetFloat(m_AnimatorAxisXName, _AxisX);        
        
        if(m_AnimatorAxisYName != "")
            animator.SetFloat(m_AnimatorAxisYName, _AxisY);

        if( _AxisX > 0f && !m_IsFacingRight || _AxisX < -0f && m_IsFacingRight){
            m_IsFacingRight = !m_IsFacingRight;               
        }

        sprite.flipX = !m_IsFacingRight;
        rb.velocity = new Vector2(_AxisX * m_Speed, rb.velocity.y);                                     
    }

    private void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 1);
        Gizmos.DrawSphere(groundCheck.position, checkRadius);    
    }

    void Update(){
        if(m_IsGrounded && Input.GetButtonDown("Jump"))
           rb.velocity = Vector2.up * m_JumpForce;
    }
}
