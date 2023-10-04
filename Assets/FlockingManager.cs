using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFish;
    public Vector3 swimLimits;
    public bool bounded;
    public bool randomize;
    public bool followLider;
    public GameObject lider;

    [Header("Fish Settings")]
    [Range(0.0f, 10.0f)]
    public float minSpeed;
    [Range(0.0f, 10.0f)]
    public float maxSpeed;
    [Range(0.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 10.0f)]
    public float rotationSpeed;

    public GameObject[] allFish;

    public float cohesionLevel;
    public float alignLevel;
    public float separationLevel;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f)); // random position
            Vector3 randomize = new Vector3(Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f), Random.Range(0.0f, 10.0f)).normalized;// random vector direction
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize));
            allFish[i].GetComponent<FlockScript>().myManager = this;  
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
