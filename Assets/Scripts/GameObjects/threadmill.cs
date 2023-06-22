using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threadmill : MonoBehaviour
{

    private List<GameObject> objectsOnTheMill;

    [SerializeField]
    private Vector3 millDirection;

    private void Start()
    {
        objectsOnTheMill = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        foreach(GameObject ob in objectsOnTheMill)
        {
            Vector3 newPosition = ob.transform.position + millDirection * Time.deltaTime;
            ob.transform.position = newPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        objectsOnTheMill.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        objectsOnTheMill.Remove(collision.gameObject);
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(millDirection);
        }
    }
}
