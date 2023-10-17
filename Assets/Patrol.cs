using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Patrol : MonoBehaviour
{
    public Transform[] points;
    public float slowingDistance = 0.5f; // Adjust this value for slowing down

    private int destPoint = 0;
    public NavMeshAgent agent;

    public bool stopPatrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        stopPatrolling = false;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        if (!agent.pathPending)
        {
            // Calculate the distance between the agent and the target.
            float distanceToTarget = Vector3.Distance(transform.position, points[destPoint].position);

            if (distanceToTarget < slowingDistance)
            {
                // Reduce the agent's speed when it gets close to the target.
                float newSpeed = agent.speed * (distanceToTarget / slowingDistance);
                agent.speed = Mathf.Max(newSpeed, 0.5f); // Ensure a minimum speed
            }
            else
            {
                // Reset the agent's speed to its original value.
                agent.speed = agent.speed;
            }

            if (agent.remainingDistance < 0.1f && !stopPatrolling)
            {
                GotoNextPoint();
            }
        }
    }


}
