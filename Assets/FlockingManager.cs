using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FlockingManager : MonoBehaviour
{
    public static FlockingManager myManager;


    public GameObject fishPrefab;
    public int numFish;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5.0f, 5.0f, 5.0f); public GameObject Lider;

    [Range(1.0f, 10.0f)]
    public float minSpeed;
    [Range(1.0f, 10.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(1.0f, 10.0f)]
    public float rotationSpeed;


    //[Range(-1f, 1f)]
    //public float COH_W;
    //[Range(-1f, 1f)]
    //public float AL_W;
    //[Range(-1f, 1f)]
    //public float SEP_W;


    // Start is called before the first frame update
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

    }


    // Update is called once per frame
    void Update()
    {

        //swimlimits

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

}
