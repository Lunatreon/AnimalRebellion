using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple Data container where the Position and the idle time can be set for enemies that reach that point
 */
public class PatrolPoints : MonoBehaviour
{
    [Tooltip("The Time an Enemy stays on this Point when reached")]
    public float idleTimeOnCheckpointReached = 0f;
}
