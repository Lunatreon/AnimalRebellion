using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * move object which are on the threadmile to a direction.
 */
public class Threadmill : SwitchButtonObjects
{

    private List<GameObject> objectsOnTheMill;

    [Tooltip("Direction of the mill, where the object should be moved")]
    [SerializeField]
    private Vector3 millDirection;
    private bool shouldMove = true;

    private void Start()
    {
        objectsOnTheMill = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            foreach (GameObject ob in objectsOnTheMill)
            {
                Vector3 newPosition = ob.transform.position + millDirection * Time.deltaTime;
                ob.transform.position = newPosition;
            }
        }
    }

    /*
     * adding the object to the objects to move
     */
    private void OnCollisionEnter(Collision collision)
    {
        objectsOnTheMill.Add(collision.gameObject);
    }

    /*
     * remove the object & give it a litte force to fake a litte physics
     */
    private void OnCollisionExit(Collision collision)
    {
        objectsOnTheMill.Remove(collision.gameObject);
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(millDirection);
        }
    }
    public override void TriggerChanged(bool switchInput)
    {
        shouldMove = !switchInput;
    }
}
