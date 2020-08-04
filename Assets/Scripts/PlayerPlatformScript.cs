using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    [Header("Movement Variables")]
    public float m_Speed = 5f;
    public float m_JumpForce = 10f;


    [Header("Animator Variables")]
    public string m_AnimatorAxisXName;
    public string m_AnimatorAxisYName;
    
    private float m_AxisX;
    private float m_AxisY;
    private bool m_IsJumping = false;
    public bool m_IsGrounded = true;

    private void Start() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
            m_IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
            m_IsGrounded = false;
    }

    private void FixedUpdate() {

        m_AxisX = Input.GetAxis("Horizontal");
        m_AxisY = rigidbody.velocity.y;       
        Debug.Log(m_AxisY);

        animator.SetFloat(m_AnimatorAxisXName, m_AxisX);        

        if(m_AxisX < -0.2f){
            sprite.flipX = true;
        } 
        else if(m_AxisX > 0.2f) {
            sprite.flipX = false;
        }

        if(m_AxisY < -0.5f)
            animator.SetBool("isFalling", true);
        else
            animator.SetBool("isFalling", false);

        if(m_IsGrounded && Input.GetButtonDown("Jump"))
            m_IsJumping = true;                     
        else
            m_IsJumping = false;        

        animator.SetBool("isJumping", m_IsJumping);
        animator.SetFloat(m_AnimatorAxisXName, m_AxisX);        

        rigidbody.velocity = new Vector2(m_AxisX * m_Speed, rigidbody.velocity.y);

        if(m_IsJumping)
             rigidbody.velocity = Vector2.up * m_JumpForce;
             
        
    }
}
