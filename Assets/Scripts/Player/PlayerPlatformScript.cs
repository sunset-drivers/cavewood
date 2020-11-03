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
        public float m_KnockbackForce = 6f;

    [Header("Jump Variables")]
        private Vector2 m_JumpDirection;
        public float m_JumpForce = 10f;        

    [Header("Animator Variables")]
        public string m_AnimatorAxisXName;
        public string m_AnimatorAxisYName;

    [Header("Layers")]
        public LayerMask m_PlayerLayer;
        public LayerMask m_InvulnerableLayer;

    [Header("State Variables")]        
        public bool m_WasDamaged = false;
        public float m_KnockbackTime = 0.15f;
        public bool m_IsFacingRight = true; 
        public bool m_IsInvulnerable = false;
        public float m_InvulnerabilityTime = 1f;        
#endregion

#region Coroutines
    private IEnumerator Knockback(){           
        m_WasDamaged = true;        
        yield return new WaitForSeconds(m_KnockbackTime);        
        m_WasDamaged = false;
    }    

    private IEnumerator Invulnerable(){           
        this.gameObject.layer = LayerMask.NameToLayer("Invulnerable");     
        m_IsInvulnerable = true;
        yield return new WaitForSeconds(m_InvulnerabilityTime);        
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        m_IsInvulnerable = false;        
    }    
#endregion

#region Runtime Events
    private void Awake() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    void Update(){
        m_AxisX = Input.GetAxis("Horizontal");        
        bool _Jumped = Input.GetButtonDown("Jump");                

        SetAnimatorParameters(m_AxisX, m_AxisY);

        if(!m_WasDamaged){         
            if(m_IsGrounded && _Jumped)
                Jump();        

            if(m_AxisX > 0f && !m_IsFacingRight || m_AxisX < -0f && m_IsFacingRight)
                ChangeDirection();                          
        }
    }

    void FixedUpdate() {
        m_IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);        
        if(!m_WasDamaged){    
            rb2d.velocity = new Vector2(m_AxisX * m_Speed, rb2d.velocity.y);                                 
        }
    }
    
#endregion

#region Player Functions

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject _CollidedWith = other.gameObject;
        if(other.gameObject.CompareTag("Enemy")) {       
            Enemy _EnemyInfo = _CollidedWith.GetComponent<Enemy>();
            float _EnemyPosition = _CollidedWith.transform.position.x;
            TakeDamage(_EnemyInfo.m_Damage, _EnemyPosition, 1f);
        }
    }

    public void TakeDamage(int Damage, float EnemyHorizontalPosition, float KnockbackHeight = 0.5f){
        StartCoroutine("Invulnerable");
        StartCoroutine("Knockback");

        StateManager.Instance.LoseMorale(Damage);

        Vector2 _Direction = (transform.position.x - EnemyHorizontalPosition > 0) 
        ? Vector2.right
        : Vector2.left;
         
        rb2d.AddForce(_Direction * m_KnockbackForce, ForceMode2D.Impulse);   
    }

    private void Jump(){        
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

}
