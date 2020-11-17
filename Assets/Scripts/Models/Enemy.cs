using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public Transform m_GizmoSensorChecker;
    public int m_Life;
    public int m_Damage;         
    public float m_KnockbackForce = 5f;
    public bool m_CanTakeDamage = true;
    public bool m_CanAttack = true;
    public float m_InvulnerabilityDuration = 0.15f;        
    public float m_AttackCountdown = 3f;   
    public GameObject m_RootEnemy;
    private Rigidbody2D m_Rigidbody;
    private GameObject m_Player;
    public float m_RayDistance = 0.5f;

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody2D>();        
        m_RootEnemy = FindRootGameObject();
    }

    public IEnumerator Damaged() {
        m_CanTakeDamage = false;
        yield return new WaitForSeconds(m_InvulnerabilityDuration);
        m_CanTakeDamage = true;
    }

    public IEnumerator Attacked() {
        m_CanAttack = false;
        yield return new WaitForSeconds(m_AttackCountdown);
        m_CanAttack = true;
    }

    public GameObject FindRootGameObject(){
        GameObject _result = this.gameObject;

        while(true){
            Transform _father = _result.transform.parent;
            if(_father != null)
                _result = _father.gameObject;
            else            
                break;
        }

        return _result;
    }
    
    public void Knockback(){
        m_Player = GameObject.FindWithTag("Player");
        Vector2 _Direction = (transform.position.x - m_Player.transform.position.x > 0) 
        ? Vector2.right
        : Vector2.left;

        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.AddForce(_Direction * m_KnockbackForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(int Damage){
        m_Life -= Damage;

        if(m_Life <= 0){
            Die();
        } else {
            StartCoroutine("Damaged");  
            Knockback();          
        }
    }       

    public void Die(){
        Destroy(m_RootEnemy);
    }

    private void OnDrawGizmos() {
        if(m_GizmoSensorChecker != null)
        Gizmos.DrawWireSphere(m_GizmoSensorChecker.position, 0.1f);
    }

}
