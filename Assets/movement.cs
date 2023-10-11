using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float radius;
    public float offset;
    private float freq;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        transform.position = new Vector3(Random.Range(-15, 15), 0, Random.Range(15, -15) / 2);
        transform.rotation = Quaternion.EulerRotation(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {

        freq += Time.deltaTime;
        if (freq > 1.5)
        {
            freq -= 1.5f;
            Wander();
        }
        
    }

    private void Wander()
    {
        offset = UnityEngine.Random.insideUnitCircle.x;
        // parameters: float radius, offset;
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);

        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;


        agent.destination = worldTarget;
    }
}
