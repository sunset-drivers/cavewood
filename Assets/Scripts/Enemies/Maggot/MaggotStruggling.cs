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
        if(m_EnemyScript.m_CanTakeDamage) {
            Struggle();
        }
    }

    public void Struggle() {
        float _HorizontalDirection; 
        if (m_Body.transform.position.x - m_Player.transform.position.x < 0) {
            _HorizontalDirection = 1f;
            m_Body.transform.eulerAngles = new Vector3(
                m_Body.transform.eulerAngles.x,
                0.0f,
                m_Body.transform.eulerAngles.z
            );
        } else {
            _HorizontalDirection = -1f;            
            m_Body.transform.eulerAngles = new Vector3(
                m_Body.transform.eulerAngles.x,
                180f,
                m_Body.transform.eulerAngles.z
            );
        }
        
        m_Rigidybody.velocity = new Vector2(
            _HorizontalDirection * m_Speed,
            m_Rigidybody.velocity.y
        );   
    }
}
