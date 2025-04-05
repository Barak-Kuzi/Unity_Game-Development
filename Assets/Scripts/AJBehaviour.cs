using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AJBehaviour : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject target;
    public GameObject point1;
    public GameObject point2;
    bool goes_to_pt1 = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetInteger("State", 1);
            //compute the path to the target and starts moving NPC
            agent.SetDestination(target.transform.position);
            if(agent.isStopped)
            {
                agent.isStopped = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.X) || distance < 2)
        {
            animator.SetInteger("State", 0);
            agent.isStopped = true;
        }
        if(distance < 3)
        {
            if(goes_to_pt1 == true)
            {
                target.transform.position = point2.transform.position;
                agent.SetDestination(target.transform.position);
                goes_to_pt1 = false;
            }
            else
            {
                target.transform.position = point1.transform.position;
                agent.SetDestination(target.transform.position);
                goes_to_pt1 = true;
            }
        }
    }
}
