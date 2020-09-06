using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformScript : MonoBehaviour
{
#region Variables
    [Header("Components")]
        private Rigidbody2D rb2d;
        private Animator animator;
        private SpriteRenderer sprite;

    [Header("Sensors")]
        public Transform groundCheck;
        public float checkRadius;
        public LayerMask whatIsGround;
        public GameObject hit;
        public bool m_IsGrounded = false;

    [Header("Movement Variables")]     
        private float m_AxisX = 0.0f;
        private float m_AxisY = 0.0f;
        public float m_Speed = 5f;

    [Header("Jump Variables")]
        private Vector2 m_JumpDirection;
        public float m_JumpForce = 10f;        

    [Header("Animator Variables")]
        public string m_AnimatorAxisXName;
        public string m_AnimatorAxisYName;

    [Header("State Variables")]        
        public bool m_IsFacingRight = true; 
#endregion

#region PlayerFunctions
    private void Jump(){
        //rb2d.velocity = Vector2.up * m_JumpForce; 
        rb2d.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Force);           
    }

    private void SetAnimatorParameters(float xAxisValue, float yAxisValue){
        if(m_AnimatorAxisXName != "")
            animator.SetFloat(m_AnimatorAxisXName, xAxisValue);            
        if(m_AnimatorAxisYName != "")
            animator.SetFloat(m_AnimatorAxisYName, yAxisValue);
    }

    private void ChangeDirection() {
        m_IsFacingRight = !m_IsFacingRight; 
        Vector3 localscale = transform.localScale;
        transform.localScale = new Vector3(localscale.x * -1, localscale.y, localscale.z);
    }    
#endregion

#region RuntimeEvents
    private void Awake() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    void Update(){
        m_AxisX = Input.GetAxis("Horizontal");        
        bool _Jumped = Input.GetButtonDown("Jump");                

        SetAnimatorParameters(m_AxisX, m_AxisY);

        if(m_IsGrounded && _Jumped)
            Jump();        

        if(m_AxisX > 0f && !m_IsFacingRight || m_AxisX < -0f && m_IsFacingRight)
            ChangeDirection();                       
    }

    void FixedUpdate() {
        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);        
        rb2d.velocity = new Vector2(m_AxisX * m_Speed, rb2d.velocity.y);                                 
    }

    public void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 1);
        Gizmos.DrawSphere(groundCheck.position, checkRadius);    
    }
#endregion
}
