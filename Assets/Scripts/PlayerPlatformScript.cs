using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    public float m_Speed = 5f;

    [Header("Animator Variables")]
    public string m_AnimatorAxisXName;
    public string m_AnimatorAxisYName;

    private float m_AxisX;
    private float m_AxisY;

    private void Start() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    private void Update() {
        m_AxisX = Input.GetAxis("Horizontal");

        if(m_AxisX < .5f)
            sprite.flipX();

        m_AxisY = Input.GetAxis("Vertical");        
    }

    private void FixedUpdate() {
        rigidbody.velocity = new Vector2(m_AxisX * m_Speed, m_AxisY);
    }
}
