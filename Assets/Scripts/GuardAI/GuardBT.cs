using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GuardBT: BehaviorTree.Tree
{
    public Transform[] waypoints;

    public static float normalSpeed = 2f;
    public static float chasingSpeed = 4f;

    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform, waypoints);
        return root;
    }
}