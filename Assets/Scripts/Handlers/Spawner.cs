using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject {
        public GameObject Prefab;        
        public int MaxQuantity;
    };

    private Collider2D m_SpawnRange;
    public SpawnableObject[] m_Spawnables;

    private void Awake() {
        m_SpawnRange = GetComponent<Collider2D>();
        
    }

    private void Start(){
        StartCoroutine("SpawnPrefabs");
    }

    private Vector3 GetRandomPosition(Vector3 ObjectSize){
        Bounds _RangeLimit = m_SpawnRange.bounds;
        Vector3 _RandomPosition = new Vector3(            
            Random.Range(ObjectSize.x - _RangeLimit.min.x, ObjectSize.x - _RangeLimit.max.x),
            Random.Range(ObjectSize.y - _RangeLimit.min.y, ObjectSize.y - _RangeLimit.max.y),
            0.0f
        );          

        return _RandomPosition;
    }

    private IEnumerator SpawnPrefabs(){
        yield return new WaitForSeconds(3);
        foreach(SpawnableObject obj in m_Spawnables){            
            int i = 0;
            do {
                Instantiate(obj.Prefab, GetRandomPosition(obj.Prefab.transform.localScale), transform.rotation);
                i++;
                yield return null;
            } while(i < obj.MaxQuantity);
        }
    }
}
