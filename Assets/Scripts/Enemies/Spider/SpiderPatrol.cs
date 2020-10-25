using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatrol : BaseFSM
{
    private Animator m_Animator;         
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_Animator = animator;                        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  
        CheckIfIsFacingRight();

        if(m_EnemyScript.m_CanTakeDamage) {
            if(m_EnemyScript.m_CanAttack && PlayerWasFound()) {
                m_Animator.SetTrigger("Attack");   
            } else {
                CheckDirectionChange();        
                Patrol();                
            }          
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        m_Rigidybody.velocity = Vector2.zero;       
    }

    public bool PlayerWasFound(){
        Vector2 _RayCastDirection = (m_IsFacingRight) 
        ? Vector2.right : Vector2.left; 

        RaycastHit2D _hit = Physics2D.Raycast(
            m_Body.transform.position, 
            _RayCastDirection, 
            m_VisionDistance,
            m_PlayerLayer            
        );
        
        return (_hit.collider != null) ? true : false;              
    }

    public void CheckDirectionChange(){
        RaycastHit2D _WallCheck = Physics2D.CircleCast(
            m_WallChecker.transform.position, 
            0.05f, 
            Vector2.zero,
            0f,
            m_GroundLayer
        );        

        RaycastHit2D _GroundCheck = Physics2D.CircleCast(
            m_GroundChecker.transform.position, 
            0.05f, 
            Vector2.zero,
            0f,
            m_GroundLayer
        );

        if(_WallCheck.collider != null || _GroundCheck.collider == null) {            
            m_Animator.SetTrigger("Idle");            
        }

    }

    public void Patrol() {   
        float _HorizontalDirection = (m_IsFacingRight) ? 1f : -1f;      
        
        m_Rigidybody.velocity = new Vector2(
            _HorizontalDirection * m_Speed,
            m_Rigidybody.velocity.y
        );             
    }
}
