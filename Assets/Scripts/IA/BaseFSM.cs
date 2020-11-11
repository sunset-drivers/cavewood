using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseFSM : StateMachineBehaviour
{
    public float m_Speed;
    public float m_Accuracy;
    public float m_VisionDistance = 1.0f;     
    public bool m_IsFacingRight;
    public Transform m_Sensors;
    public Transform m_WallChecker;
    public Transform m_GroundChecker;  
    public Enemy m_EnemyScript;
    public LayerMask m_PlayerLayer;
    public LayerMask m_GroundLayer;
    public GameObject m_Brain;
    public GameObject m_Body;
    public Rigidbody2D m_Rigidybody;
    public GameObject m_Player;
    public int m_LastWaypoint = 0;    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Brain = animator.gameObject;        
        m_Body = m_Brain.transform.Find("Body").gameObject;
        m_EnemyScript = m_Body.GetComponent<Enemy>();
        m_Rigidybody = m_Body.GetComponent<Rigidbody2D>();        
        m_Sensors = m_Body.transform.Find("Sensors");
        m_WallChecker = m_Sensors.Find("WallChecker");
        m_GroundChecker = m_Sensors.Find("GroundChecker");
        m_Player = GameObject.FindGameObjectWithTag("Player");
        
        CheckIfIsFacingRight();
    }

    public void CheckIfIsFacingRight(){
        m_IsFacingRight = (m_Body.transform.eulerAngles.y == 180f);
    }
}
