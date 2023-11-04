using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIVision : MonoBehaviour
{
    public Camera frustum;
    public LayerMask mask;
    public NavMeshAgent agent;
    public GameObject player;

    Animator animator;
    bool isWalking;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("player");
        animator = GetComponent<Animator>();
        isWalking = false;
    }
    void Update()
    {

        animator.SetBool("IsWalking", isWalking);

        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject.CompareTag("player"))
                    {
                        SendMessageUpwards("CallToAllZombies");
                    }
                }
            }
        }
    }

    public void StartFollowPlayer()
    {
        // Go to the last player registered position
        agent.destination = player.transform.position;
        isWalking = true;
    }
}
