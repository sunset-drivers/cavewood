using UnityEngine;
using UnityEngine.SceneManagement;
using Cavewood.Models;
using Cavewood.Utils;
public class OverworldEnemy: MonoBehaviour
{
    [Header("Movement Variables")]        
        public float m_MaxSpeed = 8f;
        public float m_MaxForce = 5f;    

    [Header("Start Seek Area")]
        public float m_SeekAreaRadius = 1f;        

    [Header("Components Variables")]
        public Transform m_Player;
        private Rigidbody2D rb2d;
        private ChangeScene m_ChangeScene;


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            State _CurrentState = StateManager.Instance.GetState();                
            _CurrentState.SceneName = SceneManager.GetActiveScene().name;            
            _CurrentState.PlayerPosition = other.gameObject.transform.position;
            StateManager.Instance.SetState(_CurrentState);

            m_ChangeScene.LoadLevel(m_ChangeScene.m_SceneName);
            gameObject.SetActive(false);
        }
            
    }

#region Steering Behaviours
    #region Seek
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

        private void Seek() {
            if(transform.position != m_Player.position){            
                Vector2 _DesiredVelocity = Vector2ExtraMath.Normalize(m_Player.position - transform.position)  * m_MaxSpeed;            
                Vector2 _Steering = _DesiredVelocity - rb2d.velocity;
                _Steering = Vector2.ClampMagnitude(_Steering, m_MaxForce);
                _Steering = _Steering / rb2d.mass;
                rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity + _Steering, m_MaxSpeed);
            }
        }    
    #endregion
    #region Wander
        private void Wander(){

        }
    #endregion
#endregion 
   

    private void Awake() {        
        rb2d = GetComponent<Rigidbody2D>();    
        m_ChangeScene = GetComponent<ChangeScene>();
    }

    void FixedUpdate()
    {        
        if(PlayerEnteredSeekArea())
            Seek();
        else
            Wander();
    }

    private void OnDrawGizmos() {
        Color color = new Color(255, 0, 0, 1);
        Gizmos.DrawSphere(transform.position, m_SeekAreaRadius);
    }
}
