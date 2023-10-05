using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Flocking : MonoBehaviour
{
    public float speed;
    bool turning = false;

    void Start()
    {
        speed = Random.Range(FlockingManager.myManager.minSpeed, FlockingManager.myManager.maxSpeed);
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
        else
        {

            if (Random.Range(0, 100) < 10)
            {

                speed = Random.Range(FlockingManager.myManager.minSpeed, FlockingManager.myManager.maxSpeed);
            }


            if (Random.Range(0, 100) < 10)
            {
                ApplyRules();
            }
        }
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
    private void ApplyRules()
    {

        GameObject[] allFish;
        allFish = FlockingManager.myManager.allFish;

        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
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
                    align += (this.transform.position - go.transform.position);
                    num++;
                    separation -= (transform.position - go.transform.position) / (distance * distance);

                }
            }
        }

        if (num > 0)
        {

            cohesion = (cohesion / num - transform.position).normalized * speed;
            align /= num;
            speed = Mathf.Clamp(align.magnitude, FlockingManager.myManager.minSpeed, FlockingManager.myManager.maxSpeed);


            Vector3 direction = (cohesion + align) - transform.position;
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

