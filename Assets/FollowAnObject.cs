using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class FollowAnObject : MonoBehaviour
{
    public GameObject target;
    public float maxVelocity;
    [SerializeField]
    private float velocity;
    private float startVelocity = 0f;
    public float acceleration;
    public float turnSpeed;
    float freq = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Seek();
    }

    private void Seek()
    {
        // Seek
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;    // (x, z): position in the floor

        Vector3 movement = direction.normalized * maxVelocity;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y

        Vector3.Distance(target.transform.position, transform.position);
    

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                                      Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * maxVelocity * Time.deltaTime;
    }
}
