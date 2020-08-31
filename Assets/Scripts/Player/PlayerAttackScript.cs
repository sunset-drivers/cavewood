using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float HitTime = 0.5f;
    public float m_radius = 1f;
    public LayerMask m_LayerMask;
    protected CircleCollider2D m_collider;
    protected Collider2D[] m_CollidedWith;
    protected ContactFilter2D contactFilter;
    
    void OnEnable(){               
        m_collider = GetComponent<CircleCollider2D>();                
        Vector2 _position = new Vector2(transform.position.x, transform.position.y);
        m_CollidedWith = Physics2D.OverlapCircleAll(_position, m_radius, m_LayerMask);
        int _hitCount = m_CollidedWith.Length;
        Debug.Log("HitCount: " + _hitCount);
        for(int i = 0; i < _hitCount; i++){
            Debug.Log(m_CollidedWith[i].gameObject.name);
        }
        StartCoroutine("HitWaitTime");        
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(255, 0, 0, 1);
        Gizmos.DrawSphere(transform.position, m_radius);    
    }

    IEnumerator HitWaitTime(){        
       yield return new WaitForSeconds(HitTime);
       gameObject.SetActive(false);
    }
}
