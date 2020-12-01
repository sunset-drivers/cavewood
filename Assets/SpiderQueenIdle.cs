using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderQueenIdle : StateMachineBehaviour
{
    public float m_AttackCountdownTime = 3.0f;
    public float m_Counter = 0.0f;
    public float m_Life = 300f;
    public bool m_Attacked = false;
    public Enemy m_EnemyScript;
    public GameObject m_SQHead;
    public GameObject m_SpiderQueen;
    public Animator m_SQHeadAnim;
    public Animator m_BrainAnim;
    public GameObject m_Player;
    public GameObject m_LeftLegFeet;
    public GameObject m_RightLegFeet;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_SQHead = GameObject.Find("Head");
        m_SQHeadAnim = m_SQHead.transform.Find("Sprite").GetComponent<Animator>();
        m_BrainAnim = GameObject.Find("QueenBrain").GetComponent<Animator>();
        m_LeftLegFeet = GameObject.Find("Legs").transform.Find("LeftLeg").Find("LegJount").Find("LegFeet").gameObject;
        m_RightLegFeet = GameObject.Find("Legs").transform.Find("RightLeg").Find("LegJount").Find("LegFeet").gameObject;
        m_EnemyScript = m_SQHead.GetComponent<Enemy>();

        m_RightLegFeet.GetComponent<Collider2D>().isTrigger = true;
        m_LeftLegFeet.GetComponent<Collider2D>().isTrigger = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Life = m_EnemyScript.m_Life;        
        
        if (m_Life > 0)
            m_SQHeadAnim.SetBool("IsAlive", true);
        else
        {
            m_SQHeadAnim.SetBool("IsAlive", false);
            m_BrainAnim.SetTrigger("Dying");
            Destroy(m_SQHead.GetComponent<Enemy>());
            Destroy(m_SQHead.GetComponent<Collider2D>());
            Destroy(m_SQHead.GetComponent<SpiderQueenScreamControl>());
        }            

        if (CheckIfIsLookingRight()) {
            m_SQHeadAnim.SetBool("LookingRight", true);
            m_SQHeadAnim.SetBool("LookingLeft", false);

            if (m_EnemyScript.m_CanAttack)
            {
                m_RightLegFeet.GetComponent<Collider2D>().isTrigger = false;
                m_BrainAnim.SetTrigger("AttackRight");
                m_EnemyScript.ExecuteAttack();
            }
        }
        else {
            m_SQHeadAnim.SetBool("LookingLeft", true);
            m_SQHeadAnim.SetBool("LookingRight", false);

            if(m_EnemyScript.m_CanAttack)
            {
                m_LeftLegFeet.GetComponent<Collider2D>().isTrigger = false;
                m_BrainAnim.SetTrigger("AttackLeft");
                m_EnemyScript.ExecuteAttack();
            }                
        }                    
    }

    public bool CheckIfIsLookingRight()
    {
        float _DistanceFromPlayer = (m_SQHead.transform.position.x - m_Player.transform.position.x);
        bool _IsLookingRight = (_DistanceFromPlayer > 0) ? true : false;
        return !_IsLookingRight;
    }
}
