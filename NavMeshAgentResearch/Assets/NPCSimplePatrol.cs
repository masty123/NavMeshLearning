using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSimplePatrol : MonoBehaviour

   
{   //Dictates whether theagent waits on each node;
    [SerializeField] bool patrolWait;

    //Total time that the patrol wait on each node
    [SerializeField] float totalwaitTime;
   
    //Probality of switching the patrol node
    [SerializeField] float switchProbalitity = 0.2f;
   
    //List of all the patrol points
    [SerializeField] List<Waypoint> patrolPoint;

    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool isTravel;
    bool isWaiting;
    bool patrolForward;
    float waitTimer;
   
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.Log("no mesh agent");
        }
        else
        {
            if(patrolPoint != null &&  patrolPoint.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("not enough patrolpoint");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if we're close to the destination.
        if (isTravel && navMeshAgent.remainingDistance <= 1.0f)
        {
            isTravel = false;

            //wait?
            if(isWaiting)
            {
                isWaiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

        }
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalwaitTime)
            {
                isWaiting = true;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (patrolPoint != null)
        {
            Vector3 targetVector = patrolPoint[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            isTravel = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= switchProbalitity)
        {
            patrolForward = !patrolForward;
        }

        if (patrolForward)
        {

            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoint.Count;
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoint.Count - 1;
            }
        }
    }

}
