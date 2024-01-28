using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChickenAI : MonoBehaviour
{
    public float minDist = 1f;
    public float runDistance = 3f;
    public float wanderCooldown = 2f;
    public float wanderDistance = 1f;
    public bool panicked = true;

    [SerializeField] private Transform target;
    [SerializeField] private Transform plane;

    private NavMeshAgent navAgent;
    private Transform trans;
    private float wanderTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = trans.position - target.position;
        Vector3 finalDestination = trans.position;

        if (navAgent.remainingDistance > navAgent.stoppingDistance && panicked)
            return;

        if (diff.magnitude <= minDist)
        {
            Vector3 idealDestination = diff.normalized * runDistance + trans.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(idealDestination, out hit, runDistance, NavMesh.AllAreas);

            if (Vector3.Distance(hit.position, trans.position) <= minDist)
            {
                Vector3 playerAngle = target.position - hit.position;
                Vector3 localPoint = hit.position - plane.position;
                Vector3 vec1 = localPoint.x > localPoint.z ? Vector3.forward : Vector3.right;
                Vector3 vec2 = localPoint.x > localPoint.z ? -Vector3.forward : -Vector3.right;
                float angleA = Vector3.Angle(playerAngle, vec1);
                float angleB = Vector3.Angle(playerAngle, vec2);

                Vector3 newDir = Vector3.Slerp(angleA > angleB ? vec1 : vec2, playerAngle, 0.5f);
                idealDestination = newDir.normalized * runDistance + trans.position;
            }

            finalDestination = idealDestination;
            panicked = true;
        }
        else
        {
            wanderTimer += Time.deltaTime;
            panicked = false;

            if (wanderTimer < wanderCooldown)
                return;

            wanderTimer = 0f;
            finalDestination = Random.onUnitSphere * wanderDistance + trans.position;
        }

        navAgent.SetDestination(finalDestination);
    }
}
