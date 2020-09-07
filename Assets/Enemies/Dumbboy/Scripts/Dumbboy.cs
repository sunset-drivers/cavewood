using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cavewood.Models;
public class Dumbboy : MonoBehaviour, IEnemy
{
    public float m_Life = 20f;
    public float m_Damage;
    private Animator anim;   

    public void Attack(){}

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Move(){}

    public void TakeDamage(float value)
    {
        anim.SetTrigger("WasDamaged");
        m_Life -= value;
        
        if(m_Life <= 0)
            Die();
    }

    private void Awake(){
        anim = GetComponent<Animator>();
    }
}