using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackStance : BaseFSM
{
    private Rigidbody2D m_PlayerRigidbody;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_PlayerRigidbody = m_Player.GetComponent<Rigidbody2D>();
        m_Body.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  

        if(m_PlayerRigidbody.velocity != Vector2.zero){
            animator.SetTrigger("Attack");
        }

    }
}
