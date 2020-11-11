using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotStruggling : BaseFSM
{
    public Transform m_StruggleChecker;    
    public float m_StruggleRecoilHeight = 0.1f;
    private float m_StruggleTime = 0.0f;
    public float m_MaxStruggleTime = 1f;
    private float m_JumpCountdown = 0.0f;
    public float m_MaxJumpCountdown = 1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_StruggleChecker = m_Sensors.Find("StruggleChecker");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if(m_Rigidybody.velocity.x > 0.0f)
            m_Rigidybody.velocity = new Vector2(
                m_Rigidybody.velocity.x - Time.deltaTime * 10, 
                m_Rigidybody.velocity.y
            );   
        else if(m_Rigidybody.velocity.x < 0.0f)
            m_Rigidybody.velocity = new Vector2(
                m_Rigidybody.velocity.x + Time.deltaTime * 10, 
                m_Rigidybody.velocity.y
            );

        Collider2D _GroundCollision = Physics2D.OverlapCircle(m_StruggleChecker.position, 0.05f, m_GroundLayer);                    
        bool _CanStruggle = (_GroundCollision != null);        

        if(_CanStruggle){                            
            if(m_StruggleTime < m_MaxStruggleTime) {                                                
                if(m_JumpCountdown >= m_MaxJumpCountdown) {
                    m_JumpCountdown = 0.0f; 
                    m_Rigidybody.AddForce(Vector2.up * m_StruggleRecoilHeight, ForceMode2D.Impulse);                            
                }
                else {
                    m_JumpCountdown += Time.deltaTime;               
                }                    
                                          
                m_StruggleTime += Time.deltaTime;      
            }
            else {
               animator.SetTrigger("Seek");
            }             
        }        
    }
}
