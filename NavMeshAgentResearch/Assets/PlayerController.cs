using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    bool isGrounded;

    public Rigidbody rb;

    public float jumpSpeed;

    RaycastHit hit;

    Ray ray;

    private void Awake()
    {
        agent.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {   
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
           

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("" + hit.point);
                agent.SetDestination(hit.point);
            }
        }

      
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

   


    private void FixedUpdate()
    {
        if (agent.isOnOffMeshLink && isGrounded)
        {
            Jump(jumpSpeed);
            agent.updatePosition = true;

        }
    }

    private void Jump(float jumpSpeed)
    {
       
            Debug.Log("Jumping/Falling & disabled agent");
            agent.isStopped = true;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddRelativeForce(new Vector3(0f, 10f, 4f), ForceMode.Impulse);
            isGrounded = false;
            
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            if(!isGrounded)
            {
                Debug.Log("Standing & activated agent");
                isGrounded = true;
                agent.Warp(transform.position);
                agent.isStopped = false;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("" + hit.point);
                    agent.SetDestination(hit.point);
                }
            }
        }     
    }
}
