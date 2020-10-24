using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdle : BaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_Rigidybody.velocity = Vector2.zero;        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  

        if(m_CanFollowPlayer){
            Vector2 _RayCastDirection = (m_Body.transform.eulerAngles.y != 0) 
            ? Vector2.right : Vector2.left; 

            RaycastHit2D _hit = Physics2D.Raycast(
                m_Body.transform.position, 
                _RayCastDirection, 
                m_VisionDistance,
                m_PlayerLayer            
            );

            if(_hit.collider != null){
                animator.SetTrigger("Prepare");
            }

            Debug.DrawRay(m_Body.transform.position, _RayCastDirection, Color.red); 
        }            
    }
}
