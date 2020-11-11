using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformScript : MonoBehaviour
{
    [Header("Move Variables")]
    public float m_Speed = 5.0f;

    [Header("Jump Variables")]
    public float m_JumpForce = 2.0f;

    [Header("Handlers")]
    public bool m_IsWalkingRight = false;
    public bool m_IsWalkingLeft = false;

    [Header("Components")]
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

    }

    private void OnLeft(InputAction input)
    {
        m_IsWalkingRight = false;
        m_IsWalkingLeft = true;        

        if (m_IsWalkingLeft && !m_IsWalkingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            m_Rigidbody.velocity = new Vector2(m_Speed * -1, m_Rigidbody.velocity.y);
        }

        Debug.Log(input.phase);
    }

    private void OnRight(InputValue value)
    {
        m_IsWalkingRight = true;
        m_IsWalkingLeft = false;         

        if (!m_IsWalkingLeft && m_IsWalkingRight)
        {          
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            m_Rigidbody.velocity = new Vector2(m_Speed, m_Rigidbody.velocity.y);
        }      
    }

    private void OnAction()
    {
        //TODO
    }

    private void OnJump()
    {
        //TODO
    }

}
