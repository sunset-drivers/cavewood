using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cavewood.Models;

public class SwordmanScript : MonoBehaviour, ICharacter
{
#region Components
        private Animator anim;
        private Rigidbody2D rb2d;
#endregion

    [Header("Attack Variables")]
        private bool m_CanAttack = true;
        public AnimationClip m_AttackAnimation;
        public int m_Damage = 10;
        public float HitTime = 0.5f;
        public float m_HitRadius = 1f;
        public float m_HitHorizontalPosition = 0.2f;
        public float m_HitVerticalPosition = 0.0f;

    [Header("Enemy Detect Variables")]        
        public LayerMask m_DamageableLayer;    
        protected Collider2D[] m_CollidedWith;        

    public void Action() {          
        m_CanAttack = false;              
       
        Vector2 _position = new Vector2 (
            transform.position.x + (transform.localScale.x * m_HitHorizontalPosition),
            transform.position.y + (m_HitVerticalPosition)            
        );

        m_CollidedWith = Physics2D.OverlapCircleAll(_position, m_HitRadius, m_DamageableLayer);

        int _hitCount = m_CollidedWith.Length;        
        for(int i = 0; i < _hitCount; i++){            
            var _enemy = m_CollidedWith[i].gameObject.GetComponent<Enemy>();
            if(_enemy != null && _enemy.m_CanTakeDamage){
                _enemy.TakeDamage(m_Damage);
            }
            
        }
    
        anim.SetTrigger("Attacked");           
        StartCoroutine("HitWaitTime");        
    }

    private void Awake() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(Input.GetButton("Action") && m_CanAttack)
            Action();    
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 1);
        Vector3 _position = new Vector3(
            transform.position.x + (transform.localScale.x * m_HitHorizontalPosition),
            transform.position.y + (m_HitVerticalPosition),
            transform.position.z
        );
        Gizmos.DrawSphere(_position, m_HitRadius);     
    }

    IEnumerator HitWaitTime(){               
       yield return new WaitForSeconds(m_AttackAnimation.length);   
       m_CanAttack = true;    
    }
}
