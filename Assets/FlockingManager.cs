using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public static FlockingManager myManager;

    public GameObject fishPrefab;
    public int numFish;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5.0f, 5.0f, 5.0f);

    public GameObject leaderFishPrefab;
    [HideInInspector]
    public GameObject leader;

    [Range(1.0f, 10.0f)]
    public float minSpeed;
    [Range(1.0f, 10.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(1.0f, 10.0f)]
    public float rotationSpeed;

    public Color[] leaderColors; // Define your leader colors in the Inspector

    private float timeSinceLastLeaderChange = 0f;
    public float leaderChangeInterval = 2f; // Change the leader every 2 seconds (adjust as needed)

    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(
               Random.Range(-swimLimits.x, swimLimits.x),
               Random.Range(-swimLimits.y, swimLimits.y),
               Random.Range(-swimLimits.z, swimLimits.z));
            Vector3 randomize = Random.onUnitSphere;  // random vector direction normalized

            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize));
            myManager = this;
        }

        // Instantiate the initial leader
        SetRandomLeader();
    }

    void Update()
    {
        // Update the leader change timer
        timeSinceLastLeaderChange += Time.deltaTime;

        // Change the leader every leaderChangeInterval seconds
        if (timeSinceLastLeaderChange >= leaderChangeInterval)
        {
            SetRandomLeader();
            timeSinceLastLeaderChange = 0f; // Reset the timer
        }

        // Swim limits
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 pos = allFish[i].transform.position;

            if (pos.x < -swimLimits.x || pos.x > swimLimits.x)
            {
                pos.x = Mathf.Clamp(pos.x, -swimLimits.x, swimLimits.x);
            }

            if (pos.y < -swimLimits.y || pos.y > swimLimits.y)
            {
                pos.y = Mathf.Clamp(pos.y, -swimLimits.y, swimLimits.y);
            }

            if (pos.z < -swimLimits.z || pos.z > swimLimits.z)
            {
                pos.z = Mathf.Clamp(pos.z, -swimLimits.z, swimLimits.z);
            }

            allFish[i].transform.position = pos;
        }
    }

    private void SetRandomLeader()
    {
        // Randomly select a new leader from allFish
        int newLeaderIndex = Random.Range(0, numFish);
        GameObject newLeader = allFish[newLeaderIndex];

        // Reset the color of the previous leader (if it exists)
        if (leader != null)
        {
            Renderer previousLeaderRenderer = leader.GetComponent<Renderer>();
            if (previousLeaderRenderer != null)
            {
                previousLeaderRenderer.material.color = previousLeaderRenderer.GetComponent<Flocking>().originalColor;
            }
        }

        // Set a random rotation for the new leader
        Vector3 randomRotation = new Vector3(
            Random.Range(0f, 360f), // Random rotation around the X-axis
            Random.Range(0f, 360f), // Random rotation around the Y-axis
            Random.Range(0f, 360f)  // Random rotation around the Z-axis
        );

        newLeader.transform.rotation = Quaternion.Euler(randomRotation);

        // Set a random speed for the leader
        Flocking leaderFlockingScript = newLeader.GetComponent<Flocking>();
        if (leaderFlockingScript != null)
        {
            leaderFlockingScript.speed = Random.Range(minSpeed, maxSpeed);
        }

        // Assign a random color to the new leader
        Renderer newLeaderRenderer = newLeader.GetComponent<Renderer>();
        if (newLeaderRenderer != null && leaderColors.Length > 0)
        {
            Color randomColor = leaderColors[Random.Range(0, leaderColors.Length)];
            newLeaderRenderer.material.color = randomColor;
        }

        leader = newLeader;

        // Set the leader reference for all fish
        for (int i = 0; i < numFish; ++i)
        {
            Flocking fishFlockingScript = allFish[i].GetComponent<Flocking>();
            if (fishFlockingScript != null)
            {
                fishFlockingScript.leader = leader;
            }
        }
    }
}
