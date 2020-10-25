using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackStance : BaseFSM
{
    public float m_JumpForce = 50f;
    public float m_JumpDistance = 15f;   
    public bool m_IsOnTheGround = true; 
    private Rigidbody2D m_PlayerRigidbody;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CheckIfIsFacingRight();                        
        m_EnemyScript.StartCoroutine("Attacked");                     
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  
        CheckIfIsFacingRight();

        if(m_IsOnTheGround)
        {    
            Vector2 _JumpDirection = (m_IsFacingRight) 
            ? new Vector2(1f * m_JumpDistance, 1f * m_JumpForce) 
            : new Vector2(-1f * m_JumpDistance, 1f * m_JumpForce);
                    
            m_Rigidybody.AddForce(_JumpDirection, ForceMode2D.Force);     
        }

        RaycastHit2D _GroundCheck = Physics2D.CircleCast(
            m_GroundChecker.transform.position, 
            0.01f, 
            Vector2.zero,
            0f,
            m_GroundLayer
        );        

        if(_GroundCheck.collider != null && !m_IsOnTheGround){
            animator.SetTrigger("Patrol");
        } else if(_GroundCheck.collider == null) {
            m_IsOnTheGround = false;
        }    
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        m_IsOnTheGround = true;
    }
}
