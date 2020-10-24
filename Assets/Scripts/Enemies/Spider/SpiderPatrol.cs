using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatrol : BaseFSM
{
    public Transform[] m_Waypoints;
    public int m_CurrentWaypoint;     
    private Animator m_Animator;   

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_Animator = animator;
        
        m_CurrentWaypoint = m_LastWaypoint;
                
        Transform _waypoints = m_Brain.transform.Find("Waypoints");        
        for(int i = 0; i < _waypoints.childCount; ++i){
            m_Waypoints.SetValue(_waypoints.GetChild(i), i);
        }        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);  

        if(m_CanFollowPlayer) {
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
        
        Patrol();      
    }

    public void Patrol() {
        Transform _waypoint = m_Waypoints[m_CurrentWaypoint];        
        if(Vector2.Distance(_waypoint.transform.position, m_Body.transform.position) < m_Accuracy){
            m_CurrentWaypoint = ++m_CurrentWaypoint % m_Waypoints.Length;
            _waypoint = m_Waypoints[m_CurrentWaypoint];
            m_LastWaypoint = m_CurrentWaypoint;
            m_Animator.SetTrigger("Idle");
        } else {
            Seek(m_Waypoints[m_CurrentWaypoint].position);
        }                
    }

    public void Seek(Vector3 TargetPosition) {        

        float _HorizontalDirection = 0.0f;
        if((TargetPosition.x - m_Body.transform.position.x) > 0){
            _HorizontalDirection = 1f;
            m_Body.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        } else if((TargetPosition.x - m_Body.transform.position.x) < 0){
            _HorizontalDirection = -1f;
            m_Body.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        
        m_Rigidybody.velocity = new Vector2(
            _HorizontalDirection * m_Speed,
            m_Rigidybody.velocity.y
        );
    }
}
