using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockScript : MonoBehaviour
{
    public FlockingManager myManager;
    private float speed;
    public Vector3 direction;
    float freq = 0f;
    float time = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "fish") freq = 10f;
        if (this.gameObject.tag == "lider") freq = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;

        if (this.gameObject.tag == "fish")
        {
            if (freq > 1f)
            {
                freq -= 1f;
                NewMethod();
            }
        }
        if (this.gameObject.tag == "lider")
        {
            if (freq > 3f)
            {
                freq -= 3f;
                NewMethod();
            }
        }


        transform.rotation = Quaternion.Slerp(transform.rotation,
                                      Quaternion.LookRotation(direction),
                                      myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    private void NewMethod()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 liderDirection = Vector3.zero;
        int num = 0;

        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                liderDirection = (myManager.allFish[0].transform.position - transform.position);

                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<FlockScript>().direction;
                    separation -= (transform.position - go.transform.position) /
                         (distance * distance);
                    num++;
                }
            }
        }
        if (num > 0)
        {
            cohesion = (cohesion / num - transform.position).normalized * speed;
            align /= num;
            
        }

        if (this.gameObject.tag == "fish")
        {
            direction = (cohesion * myManager.cohesionLevel + align * myManager.alignLevel + separation * myManager.separationLevel + liderDirection).normalized * speed;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }

        if (this.gameObject.tag == "lider")
        {
            direction = (cohesion * myManager.cohesionLevel + align * myManager.alignLevel + separation * myManager.separationLevel).normalized * speed;
            speed = myManager.liderSpeed;
        }
    }
}
