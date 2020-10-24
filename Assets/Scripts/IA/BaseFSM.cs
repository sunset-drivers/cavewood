using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseFSM : StateMachineBehaviour
{
    public float m_Speed;
    public float m_Accuracy;
    public float m_VisionDistance = 1.0f; 
    public bool m_CanFollowPlayer = true;   
    public Enemy m_EnemyScript;
    public LayerMask m_PlayerLayer;
    public GameObject m_Brain;
    public GameObject m_Body;
    public Rigidbody2D m_Rigidybody;
    public GameObject m_Player;
    public int m_LastWaypoint = 0;    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Brain = animator.gameObject;        
        m_Body = m_Brain.transform.Find("Body").gameObject;
        m_EnemyScript = null;
        m_Rigidybody = m_Body.GetComponent<Rigidbody2D>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }
}
