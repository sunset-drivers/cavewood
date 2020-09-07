using UnityEngine;
using Cavewood.Utils;
public class OverworldEnemy: MonoBehaviour
{
    [Header("Movement Variables")]
        public float m_Speed = 5f;
        public float m_MaxSpeed = 8f;
        public float m_MaxForce = 5f;    

    [Header("Start Seek Area")]
        public float m_SeekAreaRadius = 1f;
        public LayerMask m_PlayerLayer;

    [Header("Components Variables")]
        public Transform m_Player;
        private Rigidbody2D rb2d;
        private ChangeScene m_ChangeScene;


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            m_ChangeScene.LoadLevel(m_ChangeScene.m_SceneName);
            gameObject.SetActive(false);
        }
            
    }

    private void Seek() {
        if(transform.position != m_Player.position){            
            Vector2 _DesiredVelocity = Vector2ExtraMath.NormalizeVector2(m_Player.position - transform.position)  * m_MaxSpeed;
            Vector2 _Steering = _DesiredVelocity - rb2d.velocity;
            _Steering = Vector2ExtraMath.TruncateVector2(_Steering, m_MaxForce);
            _Steering = _Steering / rb2d.mass;
            rb2d.velocity = Vector2ExtraMath.TruncateVector2(rb2d.velocity + _Steering, m_MaxSpeed);
        }
    }    
    
    private bool PlayerEnteredSeekArea(){
        Collider2D[] _CollidedWith = Physics2D.OverlapCircleAll(
            transform.position, 
            m_SeekAreaRadius            
        );

        foreach (Collider2D collider in _CollidedWith)
        {
            if(collider.gameObject.CompareTag("Player"))
                return true;
        }

        return false;
    }

    private void Awake() {        
        rb2d = GetComponent<Rigidbody2D>();    
        m_ChangeScene = GetComponent<ChangeScene>();
    }

    void FixedUpdate()
    {        
        if(PlayerEnteredSeekArea())
            Seek();
    }

    private void OnDrawGizmos() {
        Color color = new Color(255, 0, 0, 1);
        Gizmos.DrawSphere(transform.position, m_SeekAreaRadius);
    }
}
