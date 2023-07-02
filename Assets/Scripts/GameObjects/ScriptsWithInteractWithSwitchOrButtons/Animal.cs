using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Script to manage the animals in the cage outside the house
 * they should be able to escape when a event trigger, by setting the destination to the exit
 */
public class Animal : SwitchButtonObjects
{
    [Tooltip("The exit which the animals can use")]
    [SerializeField]
    private Transform positionToRun;

    [Tooltip("Running speed of the animal")]
    [SerializeField]
    private float runSpeed = 2;

    private NavMeshAgent self;

    //if the event triggers that the cage is oppend it will start to escape
    public override void TriggerChanged(bool switchInput)
    {
        if (this.gameObject.activeSelf)
        {
            self.SetDestination(positionToRun.position);
            self.speed = runSpeed;
        }   
    }

    // Start is called before the first frame update
    void Start()
    {
        self = this.GetComponent<NavMeshAgent>();
    }
}
