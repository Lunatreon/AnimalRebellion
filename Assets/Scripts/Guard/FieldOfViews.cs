using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Checks if a collider of a ceratin type is in the field of view of the Guard.
 * Also using the obstacle between both things, to realy check if the guard sees the player
 */
public class FieldOfViews : MonoBehaviour
{
    [Tooltip("The max distance the enemy can look")]
    [SerializeField] private float viewRange;

    [Tooltip("The Angle the enemy can look")]
    public int viewAreaAngle;

    [Tooltip("The direction the enemy is looking")]
    public Transform viewDirection;

    [Tooltip("Layers of the objects which are important to interact with the world, that aren't see throw")]
    public LayerMask layerMask;
    public Guard guard;

    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = viewRange;
        guard = gameObject.GetComponentInParent<Guard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckIfReact(other);
        if (other.gameObject.CompareTag("Event"))
        {
           
            guard.EventInRange(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        CheckIfReact(other);
    }

    /*
     * will check if the player is in the field of view. 
     * if it's true the guard script will be informed with the position of the player
     */
    private void CheckIfReact(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Vector3.Angle(viewDirection.forward, other.gameObject.transform.position - viewDirection.position) < viewAreaAngle)
            {
                if (Physics.Raycast(transform.position, other.gameObject.transform.position - transform.position, out RaycastHit hit, viewRange, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        guard.PlayerInView(other.transform.position);
                    }
                }
            }
        }
    }
}
