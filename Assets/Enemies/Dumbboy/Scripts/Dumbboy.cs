using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cavewood.Models;
public class Dumbboy : Enemy
{   
#region Variables
    [Header("Combat Variables")]                          
        public LayerMask m_PlayerLayerMask;
        private List<Collider2D> m_CollidedWith = new List<Collider2D>();        
    
    [Header("Components")]
        private GameObject m_Player;            
        private Animator m_SpriteAnimator;   
        private Collider2D m_AttackRange;        
        private Rigidbody2D rb2d;

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        m_SpriteAnimator = transform.Find("Sprite").gameObject.GetComponent<Animator>();
        m_AttackRange = transform.Find("AttackRange").gameObject.GetComponent<Collider2D>();
        m_Player = GameObject.FindGameObjectWithTag("Player");                               
    }    

    private void FixedUpdate() {                  
        Move();
    }
}