using System.Collections;
using UnityEngine;

namespace Cavewood.Models
{
    public abstract class Enemy : MonoBehaviour 
    {
        public int m_Health;
        public int m_Damage;     
        private float m_StaggerCountdown = 0.0f;
        public float m_MaxStaggerTime = 0.3f;
                               
        protected abstract void Move();
        protected abstract void Attack();

        IEnumerator Knocked() {
            do {
                m_StaggerCountdown += Time.deltaTime;                
                yield return null;
            } while (m_StaggerCountdown < m_MaxStaggerTime);
            m_StaggerCountdown = 0.0f;
        }

        public void TakeDamage(){
            StartCoroutine("Knockback");
        }       

        public void Die(){
            Destroy(transform.parent.gameObject);
        }
    }
}