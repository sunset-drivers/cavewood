using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public int m_Life;
    public int m_Damage;     
    public bool m_CanFollowPlayer;
    public float m_MaxStaggerTime = 0.3f;
    private float m_StaggerCountdown = 0.0f;                

    IEnumerator Knocked() {
        do {
            m_StaggerCountdown += Time.deltaTime;                
            yield return null;
        } while (m_StaggerCountdown < m_MaxStaggerTime);
        m_StaggerCountdown = 0.0f;
    }

    public void TakeDamage(int Damage){
        m_Life -= Damage;

        if(m_Life <= 0)
            Die();
        else
            StartCoroutine("Knocked");            
    }       

    public void Die(){
        Destroy(transform.parent.gameObject);
    }
}
