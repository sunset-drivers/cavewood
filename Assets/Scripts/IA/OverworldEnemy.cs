using UnityEngine;
using UnityEngine.SceneManagement;
using Cavewood.Models;
using Cavewood.Utils;
public class OverworldEnemy: MonoBehaviour
{
    [Header("Movement Variables")]        
        public float m_MaxSpeed = 8f;
        public float m_MaxForce = 5f;    

    [Header("Wander")]
        public Vector2 m_CircleDistance;
        public Vector2 m_CircleRadius;
        public float m_AngleChange;
        private float m_WanderAngle = 0.0f;

    [Header("Seek")]
        public float m_SeekAreaRadius = 1f;        

    [Header("Components Variables")]
        private Transform m_Player;
        private Rigidbody2D rb2d;
        private ChangeScene m_ChangeScene;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            State _CurrentState = StateManager.Instance.GetState();                
            _CurrentState.SceneName = SceneManager.GetActiveScene().name;            
            _CurrentState.PlayerPosition = other.gameObject.transform.position;
            _CurrentState.WasFacingRight = true;
            StateManager.Instance.SetState(_CurrentState);

            m_ChangeScene.LoadLevel(m_ChangeScene.m_NextSceneName);
            gameObject.SetActive(false);
        }
            
    }

#region Steering Behaviours
    #region Steering
        void Steer(Vector2 Force, float SpeedModifier = 1.0f){
            Vector2 _Steering = Vector2.ClampMagnitude(Force, m_MaxForce);
            _Steering = _Steering / rb2d.mass;
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity + _Steering, m_MaxSpeed/SpeedModifier);
        }
    #endregion
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
                Vector2 _Force = _DesiredVelocity - rb2d.velocity;
                Steer(_Force);
            }
        }    
    #endregion
    #region Wander
        public Vector2 SetAngle(Vector2 vector, float value) {
            float len = vector.magnitude;
            vector.x = Mathf.Cos(value) * len;
            vector.y = Mathf.Sin(value) * len;
            return vector;
        }
        private void Wander() {
            Vector2 _CircleCenter = rb2d.velocity;
            _CircleCenter.Normalize();
            _CircleCenter = Vector2.Scale(_CircleCenter, m_CircleDistance);
            
            Vector2 _Displacement = new Vector2(0, -1);
            _Displacement = Vector2.Scale(_Displacement, m_CircleRadius);
            
            _Displacement = SetAngle(_Displacement, m_WanderAngle);
            m_WanderAngle += (Random.Range(0, 90) * m_AngleChange) - (m_AngleChange * .5f);

            Vector2 m_WanderForce = _CircleCenter + _Displacement;
            Steer(m_WanderForce, 4);
        }       
    #endregion
#endregion 
   

    private void Awake() {        
        rb2d = GetComponent<Rigidbody2D>();    
        m_ChangeScene = GetComponent<ChangeScene>();
        m_Player = GameObject.Find("Player").GetComponent<Transform>();
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
