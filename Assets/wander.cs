using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wander : MonoBehaviour
{
    float radius = 8f;
    float offset = 10f;
    public NavMeshAgent agent;
    float f = 4.0f;
    Vector3 worldTarget;

    // Update is called once per frame
    void Update()
    {
        f += Time.deltaTime;

        if (f > 4.0f)
        {
            // parameters: float radius, offset;
            Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
            localTarget += new Vector3(0, 0, offset);
            worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;
            agent.destination = worldTarget;

            f = 0.0f;
        }
    }
}
