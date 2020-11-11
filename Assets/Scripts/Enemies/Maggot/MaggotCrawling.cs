using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotCrawling : BaseFSM
{
    private Animator m_Animator; 
    float m_VisionOriginPosition = 0.3f;        
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_Animator = animator;  
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  
        CheckIfIsFacingRight();
        CheckDirectionChange();         
        Patrol();    
        CheckIfPlayerIsNear();
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
            if(m_IsFacingRight){
                m_Body.transform.eulerAngles = new Vector3(
                    m_Body.transform.eulerAngles.x, 
                    0f, 
                    m_Body.transform.eulerAngles.z
                );
            } else {
                m_Body.transform.eulerAngles = new Vector3(
                    m_Body.transform.eulerAngles.x, 
                    180f, 
                    m_Body.transform.eulerAngles.z
                );
            }                   
        }

    }

    public void CheckIfPlayerIsNear() {
        Vector2 _RayCastPosition;
        if (!m_IsFacingRight) {
            _RayCastPosition = new Vector2(
                m_Body.transform.position.x + m_VisionOriginPosition,
                m_Body.transform.position.y
            );
        } else {
            _RayCastPosition = new Vector2(
                m_Body.transform.position.x - m_VisionOriginPosition,
                m_Body.transform.position.y
            );
        }                

        RaycastHit2D _hit = Physics2D.Raycast(
            _RayCastPosition, 
            Vector2.down, 
            m_VisionDistance,
            m_PlayerLayer            
        );

        if(_hit.collider != null){
            m_Rigidybody.gravityScale = 1f;            
            m_Animator.SetTrigger("Struggle");
        }
    }

    public void Patrol() {   
        float _HorizontalDirection = (m_IsFacingRight) ? -1f : 1f;      
        
        m_Rigidybody.velocity = new Vector2(
            _HorizontalDirection * m_Speed,
            m_Rigidybody.velocity.y
        );             
    }
}
