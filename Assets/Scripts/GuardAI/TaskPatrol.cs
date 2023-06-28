using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform transfrom;
    private Transform[] waypoints;
    private int currenWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        this.transfrom = transform;
        this.waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
                waiting = false;
        }
        else{
            Transform wp = waypoints[currenWaypointIndex];
            if(Vector3.Distance(transfrom.position, wp.position) < 0.01f)
            {
                transfrom.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currenWaypointIndex = (currenWaypointIndex + 1) % waypoints.Length;
            }
            else
            {
                transfrom.position = Vector3.MoveTowards(transfrom.position, wp.position, GuardBT.normalSpeed * Time.deltaTime);
                transfrom.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}
