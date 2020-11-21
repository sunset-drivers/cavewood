using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdle : BaseFSM
{       
    public bool m_FoundPlayer = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);         
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  
        CheckIfIsFacingRight();     

        if(m_EnemyScript.m_CanAttack){
            Vector2 _RayCastDirection = (m_IsFacingRight) ? Vector2.right : Vector2.left; 

            RaycastHit2D _hit = Physics2D.Raycast(
                m_Body.transform.position, 
                _RayCastDirection, 
                m_VisionDistance,
                m_PlayerLayer            
            );

            if(_hit.collider != null){                    
                m_FoundPlayer = true;
                animator.SetTrigger("Attack");
            } else {
                m_FoundPlayer = false;
            }            
        }
        else {
            m_FoundPlayer = false;
        }                   
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        ChangeDirection();
    }

    public void ChangeDirection(){
        if(!m_FoundPlayer){
            if(m_IsFacingRight){
                m_Body.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            } else {
                m_Body.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
    }
}
