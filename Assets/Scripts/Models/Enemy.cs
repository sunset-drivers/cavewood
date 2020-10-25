using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public int m_Life;
    public int m_Damage;         
    public bool m_CanTakeDamage = true;
    public bool m_CanAttack = true;
    public float m_InvulnerabilityDuration = 0.3f;        
    public float m_AttackCountdown = 3f;    

    IEnumerator Damaged() {
        m_CanTakeDamage = false;
        yield return new WaitForSeconds(m_InvulnerabilityDuration);
        m_CanTakeDamage = true;
    }

    IEnumerator Attacked() {
        m_CanAttack = false;
        yield return new WaitForSeconds(m_AttackCountdown);
        m_CanAttack = true;
    }

    public void TakeDamage(int Damage){
        m_Life -= Damage;

        if(m_Life <= 0){
            Die();
        } else {
            StartCoroutine("Damaged");            
        }
    }       

    public void Die(){
        Destroy(transform.parent.gameObject);
    }
}
