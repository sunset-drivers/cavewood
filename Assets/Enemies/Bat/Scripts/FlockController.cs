using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    [Header("Flocking")]                
        public List<GameObject> m_Boids = new List<GameObject>();
        public Vector3 m_Target = Vector3.zero;                
    [Header("Boids")]
        [Range(0.0f, 5.0f)] public float m_MinSpeed = 0.4f;
        [Range(0.0f, 5.0f)] public float m_MaxSpeed = 1.0f;
        [Range(0.0f, 10.0f)] public float m_MinRotationSpeed = 3.0f;
        [Range(0.0f, 10.0f)] public float m_MaxRotationSpeed = 6.0f;
        [Range(0.0f, 10.0f)] public float m_NeighbourDistance = 0.5f;

    void Awake(){                
        for(int i = 0; i < transform.childCount; i++)
        {                                
            Transform Child = transform.GetChild(i).Find("Body");
            Child.GetComponent<BatBehaviour>().m_Manager = this;
            m_Boids.Add(Child.gameObject);
        }
    }
}
