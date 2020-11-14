using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float delay;
    public static bool spawnClouds = true;

    void Start()
    {
        Instantiate(cloudPrefab);
    }
}
