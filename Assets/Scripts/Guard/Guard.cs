using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
   private enum GuardState
    {
        Patrol,
        CheckPosition,
        ChasePosition,
        ReturnToPatrol
    }

    [Tooltip("Default Idle Time the Guard check a area if not defined by the waypoint (or is 0)")]
    [SerializeField] private float idleTime;

    [Tooltip("The Text which should show if the Guard had seen the player or heard something")]
    [SerializeField] private TextMeshProUGUI textToShowStatus;

    [Header("Patrol")]
    [Tooltip("The patrol route of the guard. they will always start at position 0")]
    [SerializeField] private PatrolPoints[] PatrolRoute;
    [SerializeField] private float walkSpeed;
    private int patrolNumber;
    private Vector3 lastPatrolPosition;
    private Quaternion lastPatrolRotation;


    [Header("Chase")]
    [SerializeField] private float timeUntilInterestDecays;
    [Tooltip("Time the player has to be seen by the guard, until the chase begin")]
    [SerializeField] private float chaseThreshhold = 1f;
    [SerializeField] private float chaseSpeed;
    private float currentInterest = 0;
    private float decayStartTimer = 0;

    [Header("Reaction")]
    [SerializeField] private Canvas response;
    /*
    * Logic variable to store needed data and used by more than one state
    */
    private GuardState state;
    private GuardState State
    {
        get { return state; }
        set
        {
            state = value;
            switch(value)
            {
                case GuardState.Patrol:
                    StartCoroutine(Patrol());
                    break;
                case GuardState.CheckPosition:
                    StartCoroutine(CheckPosition());
                    break;
                case GuardState.ChasePosition:
                    StartCoroutine(ChasePosition());
                    break;
                case GuardState.ReturnToPatrol:
                    StartCoroutine(ReturnToPatrol());
                    break;
            }
        }
    }
   
    //Using the Navigation Mesh from unity to have an easy possibility to 
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private Animator guardAnimator;


    private void Start()
    {
        guardAnimator = gameObject.GetComponentInChildren<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        State = GuardState.Patrol;
        textToShowStatus.text = "";
    }

    private void Update()
    {
        guardAnimator.SetFloat("Walkspeed", agent.velocity.magnitude);
        guardAnimator.speed = agent.velocity.magnitude/2;
        response.transform.LookAt(Camera.main.transform);

        decayStartTimer -= Time.deltaTime;

        if (decayStartTimer <= 0 && currentInterest > 0 && State != GuardState.ChasePosition)
        {
            currentInterest -= Time.deltaTime;
        }
        //manage the information for the player (? the guard had notice you ! the guard will hunt you)
        if (currentInterest <= 0 && state != GuardState.ChasePosition && state != GuardState.CheckPosition)
        {
            textToShowStatus.text = "";
        }
        else if (currentInterest <= 0 && state == GuardState.CheckPosition)
        {
            textToShowStatus.text = "?";
            textToShowStatus.color = Color.yellow;
        }

        if (currentInterest > 0)
        {
            textToShowStatus.text = "?";
            textToShowStatus.color = Color.yellow;
            
        }
        if (currentInterest > chaseThreshhold)
        {
            textToShowStatus.text = "!";
            textToShowStatus.color = Color.red;
        }
    }


    /*
     * Coroutine that conrtols the flow, when a Guard is chased oder checked a Spot and wants to return to the patrol
     */
    private IEnumerator ReturnToPatrol()
    {
        //when entering the State
        //Set estination to last known Patrol Position
        agent.SetDestination(lastPatrolPosition);
        agent.speed = walkSpeed;

        while(State == GuardState.ReturnToPatrol)
        {
            if (ArrivedAtDestination())
            {
                transform.rotation = lastPatrolRotation;
                State = GuardState.Patrol;
            }
            yield return null;
        }
    }

    /*
     * check position state. Guard should check a given position or update it. if the guard arrived at the destination and nobody is the he will return to the patrol
     */
    private IEnumerator CheckPosition()
    {
        agent.SetDestination(targetPosition);
        agent.speed = walkSpeed;

        while(State == GuardState.CheckPosition)
        {
            //if a new "Event" trigger
            UpdateDestinationIfNew();
            //if arrived at destination returnToPatrol;
            if (ArrivedAtDestination())
            {
                yield return new WaitForSeconds(idleTime);
                State = GuardState.ReturnToPatrol;
            }
            yield return null;
        }
    }

    /*
     * Guard has seen player and chase them until he is not be seen anymore
     */
    private IEnumerator ChasePosition()
    {
        //When entering the State
        agent.SetDestination(targetPosition);
        agent.speed = chaseSpeed;

        //While in this State
        while (State == GuardState.ChasePosition)
        {
            UpdateDestinationIfNew();
            if (ArrivedAtDestination())
            {
                currentInterest = 0f;
                State = GuardState.ReturnToPatrol;
            }
            yield return null;
        }
    }

    /*
     * patrol between given points
     */
    private IEnumerator Patrol()
    {
        if(PatrolRoute.Length == 0)
        {
            lastPatrolPosition = transform.position;
            lastPatrolRotation = transform.rotation;
            yield break;
        }

        agent.SetDestination(PatrolRoute[patrolNumber].transform.position);
        agent.speed = walkSpeed;

        while(State == GuardState.Patrol)
        {
            if (ArrivedAtDestination())
            {
                yield return new WaitForSeconds(PatrolRoute[patrolNumber].idleTimeOnCheckpointReached > 0 ? PatrolRoute[patrolNumber].idleTimeOnCheckpointReached : idleTime);
                patrolNumber = (patrolNumber + 1) % PatrolRoute.Length;
                agent.SetDestination(PatrolRoute[patrolNumber].transform.position);
            }
            yield return null;
        }

        //when leaving the state to get back to the last positions
        lastPatrolPosition = transform.position;
        lastPatrolRotation = transform.rotation;
    }

    /*
     * if the guard has seen the player
     */
    public void PlayerInView(Vector3 position)
    {
        targetPosition = position;
        currentInterest += Time.deltaTime;
        if (currentInterest > chaseThreshhold && State != GuardState.ChasePosition)
        {
            State = GuardState.ChasePosition;
        }
        decayStartTimer = timeUntilInterestDecays;
    }
    
    /*
     * update position if the target position don't match the destination
     */
    private void UpdateDestinationIfNew()
    {
        if (targetPosition != agent.destination) agent.SetDestination(targetPosition);
    }

    /*
     * check if guard arrived at destination
     */
    private bool ArrivedAtDestination()
    {
        return agent.remainingDistance < 0.1f && !agent.pathPending;
    }

    /*
     * if something interesting accured in the range of the guard
     */
    public void EventInRange(Transform transform)
    {
        targetPosition = transform.position;
        lastPatrolPosition = transform.position;
        lastPatrolRotation = transform.rotation;
        State = GuardState.CheckPosition;
    }
}
