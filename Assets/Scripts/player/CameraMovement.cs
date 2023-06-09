using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float height;

    private float actDistance;
    public float maxDistance;
    public float minDinstance;
    private float rotation;
    public Vector3 playerOffset;

    private Vector3 oldMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        Vector3 dist = new Vector3(0, height, -distance);
        actDistance = dist.magnitude;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rotation += (oldMousePosition.x - Input.mousePosition.x) * Time.deltaTime * 100f;
        }

        actDistance += Input.mouseScrollDelta.y * Time.deltaTime * 10f;
        if (actDistance <= minDinstance)
        {
            actDistance = minDinstance;
        }
        else if (actDistance > maxDistance)
        {
            actDistance = maxDistance;
        }

        oldMousePosition = Input.mousePosition;
    }
    private void LateUpdate()
    {
        Vector3 dist = new Vector3(0, height, -distance);

        Vector3 trans = /*player.transform.TransformVector(*/Quaternion.Euler(0, rotation, 0) * dist.normalized/*)*/;
        transform.position = trans * actDistance + playerOffset + player.transform.position;
        transform.LookAt(playerOffset + player.transform.position, Vector3.up);
    }
}
