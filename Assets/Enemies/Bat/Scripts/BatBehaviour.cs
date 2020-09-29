using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    public float m_MaxSpeed = 5.0f;
    public float m_MaxVelocity = 1.0f; 
    public float m_MaxForce = 2.0f;   
    public float m_SensorRadius = 2.0f;
    public float m_MaxSeparationDistance = 300;
    public LayerMask m_PlayerLayer;
    private Transform m_VisibilityPosition;
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private Vector3 m_LastPlayerPosition = Vector3.zero;  
    public FlockController m_Manager;  

    private IEnumerator Echolocalization(){
        yield return new WaitForSeconds(0.2f);        
        Collider2D _SeekingPlayer = Physics2D.OverlapCircle(transform.position, m_SensorRadius, m_PlayerLayer);         
        if(_SeekingPlayer != null)
            m_Manager.m_Target = _SeekingPlayer.gameObject.transform.position;
    }   

    private void Move(Vector3 TargetPosition) {        
        Vector2 _DesiredVelocity = (TargetPosition - transform.position).normalized * m_MaxVelocity;    
        
        Vector2 _Steering = (_DesiredVelocity - m_Rigidbody.velocity);    
        _Steering = Vector2.ClampMagnitude(_Steering, m_MaxForce);
        _Steering = _Steering/m_Rigidbody.mass;
        
       // m_Rigidbody.velocity = Vector2.ClampMagnitude(m_Rigidbody.velocity + _Steering, m_MaxSpeed);        

        m_Animator.SetFloat("Speed", m_Rigidbody.velocity.x);
        if(m_Rigidbody.velocity.x < -0.2){
            transform.localScale = new Vector3(1,1,1);
        }            
        else if(m_Rigidbody.velocity.x > 0.2){
            transform.localScale = new Vector3(-1,1,1);
        }
        ApplyRules();    
    }

    private void ApplyRules(){
        if(Random.Range(0.0f, 100.0f) > 20.0f) return;

        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int groupSize = 0;
        float distance = 0.0f;
        float speed = 0.01f;

        foreach(var boid in m_Manager.m_Boids){
            if(boid == this.gameObject) continue;
            
            distance = Vector3.Distance(this.transform.position, boid.transform.position);
            if(distance <= m_Manager.m_NeighbourDistance) continue;

            groupSize++;
            cohesion += boid.transform.position;

            if(distance < 1.0f){
                separation += (this.transform.position - boid.transform.position);
            }

            speed += boid.GetComponent<BatBehaviour>().m_MaxSpeed;
        }

        if(groupSize == 0) return;
        cohesion = cohesion / groupSize + (m_Manager.m_Target - this.transform.position);
        speed = speed / groupSize;

        var direction = (cohesion + separation) - this.transform.position;
        if(direction == Vector3.zero) return;

        m_Rigidbody.velocity = direction.normalized;
    }

    private void Start() {
        m_VisibilityPosition = transform.Find("Visibility");
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = transform.Find("Sprite").GetComponent<Animator>();        
        m_MaxSpeed = Random.Range(m_Manager.m_MinSpeed, m_Manager.m_MaxSpeed);
    }    

    private void Update() {                
        StartCoroutine("Echolocalization");
        if(m_Manager.m_Target != Vector3.zero) {            
            Move(m_Manager.m_Target);
        }                  
    }
}
