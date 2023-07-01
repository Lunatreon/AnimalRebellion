using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animal : SwitchButtonObjects
{

    [SerializeField]
    private Transform positionToRun;

    [SerializeField]
    private float runSpeed = 2;

    private NavMeshAgent self;



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
