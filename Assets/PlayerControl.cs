using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Patrol patrolScript;
    private CapsuleCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("asd");
        patrolScript = GetComponent<Patrol>();
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("aaa");
        if (collision.gameObject.CompareTag("Zombie"))
        {
            patrolScript.stopPatrolling = true;
            patrolScript.agent.speed = 0;
            Debug.Log("Trigger Triggered");
            this.gameObject.SetActive(false);
        }
    }
}
