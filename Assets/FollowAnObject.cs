using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class FollowAnObject : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // Seek
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;    // (x, z): position in the floor

        // Flee
        Vector3 direction = transform.position - target.transform.position;

        Vector3 movement = direction.normalized * maxVelocity;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * maxVelocity * Time.deltaTime;

        Vector3.Distance(target.transform.position, transform.position);

        Mathf.Abs(Vector3.Angle(transform.forward, movement);  // forward = z
    }


}
