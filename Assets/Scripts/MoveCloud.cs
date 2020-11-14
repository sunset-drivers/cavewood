using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float minY;
    public float maxY;
    public float buffer;
    private float startposition;
    float speed;
    float camWidth;
    void Start()
    {
        
        startposition = gameObject.transform.position.x;
        camWidth = 150f;

        speed = Random.Range(minSpeed, maxSpeed);
        transform.position = new Vector3(-camWidth - buffer, Random.Range(minY, maxY), transform.position.z);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x - buffer > camWidth)
        {
            gameObject.transform.position = new Vector2(startposition, Random.Range(minY, maxY));
        }
    }

}
