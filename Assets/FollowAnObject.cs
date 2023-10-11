using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class FollowAnObject : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public float maxVelocity;
    [SerializeField]
    private float velocity;
    private float startVelocity = 0f;
    public float acceleration;
    public float turnSpeed;
    float freq = 0f;

    public bool useWander;

    // Start is called before the first frame update
    void Start()
    {
        useWander = true;
        transform.position = new Vector3(Random.Range(-10, 10), 0, Random.Range(10, -10) / 2);
        transform.rotation = Quaternion.EulerRotation(0, Random.Range(0, 360), 0);

    }

    void Update()
    {
        Seek();
    }

    private void Seek()
    {
        if(useWander)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            // Seek
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;    // (x, z): position in the floor

            Vector3 movement = direction.normalized * maxVelocity;

            float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y

            Vector3.Distance(target.transform.position, transform.position);
            angle = Mathf.Abs(Vector3.Angle(transform.forward, movement));  // forward = z

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                                          Time.deltaTime * turnSpeed);
            transform.position += transform.forward.normalized * maxVelocity * Time.deltaTime;
        }
        
    }
}
