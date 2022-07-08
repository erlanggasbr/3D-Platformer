using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    public enum AIState
    {
        isIdle, isPatrolling, isChasing,isAttacking
    };

    public AIState currentState;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;
    public float attackRange;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:
                if(waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.isPatrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if(distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                break;

            case AIState.isPatrolling:

                //agent.SetDestination(patrolPoints[currentPatrolPoint].position);

                if (agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    //agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }

                break;

            case AIState.isChasing:
                agent.SetDestination(PlayerController.instance.transform.position);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.isAttacking;
                    anim.SetTrigger("Attack");
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;

                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position); 
                }
                
                break;

            case AIState.isAttacking:
                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                if (distanceToPlayer < attackRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;

                    agent.isStopped = false;
                }

                break;
        }
    }
}
