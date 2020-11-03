using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotStruggling : BaseFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  
        Struggle();
    }

    public void Struggle() {
        float _HorizontalDirection = (m_Body.transform.position.normalized.x - m_Player.transform.position.normalized.x);
        
        m_Rigidybody.velocity = new Vector2(
            _HorizontalDirection * m_Speed,
            m_Rigidybody.velocity.y
        );   
    }
}
