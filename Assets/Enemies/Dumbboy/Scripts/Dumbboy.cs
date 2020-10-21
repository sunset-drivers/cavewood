using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cavewood.Models;
public class Dumbboy : MonoBehaviour, IEnemy
{   
#region Variables
    [Header("Combat Variables")]          
        public float m_Life = 20f;
        public int m_Damage = 100;        
        public float m_MaxAttackCountdown = 1.0f;
        private float m_AttackCountdown = 0.0f;  
        public float m_MaxStaggerTime = 0.5f;
        private float m_StaggerCountdown = 0.0f;  
        public float m_Speed = 3.0f; 
        public float m_KnockbackForce = 5.0f;
        public LayerMask m_PlayerLayerMask;
        private List<Collider2D> m_CollidedWith = new List<Collider2D>();        
    
    [Header("Components")]
        private GameObject m_Player;            
        private Animator m_SpriteAnimator;   
        private Collider2D m_AttackRange;        
        private Rigidbody2D rb2d;            
#endregion
    IEnumerator Knocked() {
        do {
            m_StaggerCountdown += Time.deltaTime;
            if(m_StaggerCountdown > m_MaxStaggerTime)
                m_AttackCountdown = m_MaxStaggerTime;                
            yield return null;
        } while (m_StaggerCountdown < m_MaxStaggerTime);
        m_StaggerCountdown = 0.0f;
    }

    IEnumerator Attacking() {
        do {                        
            m_AttackCountdown += Time.deltaTime;
            if(m_AttackCountdown > m_MaxAttackCountdown)
                m_AttackCountdown = m_MaxAttackCountdown;                
            yield return null;
        } while(m_AttackCountdown < m_MaxAttackCountdown);
        m_AttackCountdown = 0.0f;

        ContactFilter2D _Filter = new ContactFilter2D();
        List<Collider2D> _CollidedWith = new List<Collider2D>();        
        _Filter.SetLayerMask(m_PlayerLayerMask);        
        int _PlayerWasDamaged = m_AttackRange.OverlapCollider(_Filter, _CollidedWith);                     
        if(_PlayerWasDamaged > 0){
            m_Player.GetComponent<PlayerPlatformScript>().TakeDamage(
                m_Damage,
                5.0f,
                transform.position.x
            );
        }   
    }

    public void Attack() {                
        rb2d.velocity.Set(0.0f, rb2d.velocity.y);
        m_SpriteAnimator.SetTrigger("DidAttack");
        StartCoroutine("Attacking");                  
    }

    public void Die() {
        Destroy(transform.parent.gameObject);
    }

    public void Knockback(){    
        StartCoroutine("Knocked");             
        rb2d.velocity = new Vector2(
            GetDirection(transform.position, m_Player.transform.position), 
            0.5f
        ) * m_KnockbackForce;
    }

    public void TakeDamage(float value) {
        Knockback();
        m_SpriteAnimator.SetTrigger("WasDamaged");
        m_Life -= value;
        if(m_Life <= 0)
            Die();
    }

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        m_SpriteAnimator = transform.Find("Sprite").gameObject.GetComponent<Animator>();
        m_AttackRange = transform.Find("AttackRange").gameObject.GetComponent<Collider2D>();
        m_Player = GameObject.Find("Player");                               
    }    

    private void FixedUpdate() {  
        ContactFilter2D _Filter = new ContactFilter2D();
        _Filter.SetLayerMask(m_PlayerLayerMask);             
        int _CollidedCount = m_AttackRange.OverlapCollider(_Filter, m_CollidedWith);  
        bool _CanAttack = (m_AttackCountdown == 0.0f && _CollidedCount > 0);    
        bool _CanMove = (m_StaggerCountdown == 0.0f);   

       // if(_CanAttack)
            //Attack();    
        //else if(_CanMove)        
        if(_CanMove && !m_Player.GetComponent<PlayerPlatformScript>().IsInvulnerable())       
            Move(m_Player.transform);
    }

    public float GetDirection(Vector3 PositionA, Vector3 PositionB){
        float _Direction = 0.0f;

        if((PositionA - PositionB).normalized.x > 0)
            _Direction = 1.0f;
        else 
            _Direction = -1.0f;
               
        return _Direction;
    }

    public void Move(Transform Target)
    {        
        rb2d.velocity = new Vector2(
            GetDirection(Target.position, transform.position) * m_Speed, 
            rb2d.velocity.y
        );
    }
}