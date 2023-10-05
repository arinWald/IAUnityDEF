using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public float speed;
    bool turning = false;
    [HideInInspector]
    public GameObject leader;
    public Color originalColor;

    void Start()
    {
        speed = Random.Range(FlockingManager.myManager.minSpeed, FlockingManager.myManager.maxSpeed);
        originalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        Bounds b = new Bounds(FlockingManager.myManager.transform.position, FlockingManager.myManager.swimLimits * 2.0f);

        if (!b.Contains(transform.position))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            Vector3 direction = FlockingManager.myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                FlockingManager.myManager.rotationSpeed * Time.deltaTime);
        }
        else if (Random.Range(0, 100) < 10)
        {
            ApplyRules();
            speed = Random.Range(FlockingManager.myManager.minSpeed, FlockingManager.myManager.maxSpeed);
        }

        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);

        if (leader != null)
        {
            Vector3 directionToLeader = leader.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(directionToLeader),
                FlockingManager.myManager.rotationSpeed * Time.deltaTime);

            // Move towards the leader
            transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
        }
    }

    private void ApplyRules()
    {
        GameObject[] allFish;
        allFish = FlockingManager.myManager.allFish;

        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int num = 0;

        foreach (GameObject go in allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);

                if (distance <= FlockingManager.myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;

                    // Calculate the direction to the neighbor fish
                    Vector3 neighborDirection = go.transform.forward;

                    // Add neighbor direction to average direction
                    separation -= (transform.position - go.transform.position) / (distance * distance);
                }
            }
        }

        if (num > 0)
        {
            cohesion = (cohesion / num - transform.position).normalized * speed;
            separation /= num;

            // Apply alignment based on leader's direction
            Vector3 alignment = leader != null ? leader.transform.forward.normalized * speed : Vector3.zero;
            Vector3 direction = (cohesion + alignment) - transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    FlockingManager.myManager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
