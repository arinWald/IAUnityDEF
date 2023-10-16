using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Patrol patrolScript;
    // Start is called before the first frame update
    void Start()
    {
        patrolScript = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            patrolScript.stopPatrolling = true;
            Debug.Log("Trigger Triggered");
        }
    }
}
