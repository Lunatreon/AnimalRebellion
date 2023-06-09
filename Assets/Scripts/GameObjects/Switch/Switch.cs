using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private List<SwitchButtonObjects> objectsToTrigger;

    public GameObject switchAnimation;

    private bool switchDirection = false;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        float zValue = switchAnimation.gameObject.transform.rotation.eulerAngles.z;
        if (zValue > 180)
        {
            zValue -= 360;
        }

        if (zValue < 40 && switchDirection)
        {
            switchAnimation.gameObject.transform.Rotate(0, 0, 120 * Time.deltaTime);
        }
        else if (zValue > -40 && !switchDirection)
        {
            switchAnimation.gameObject.transform.Rotate(0, 0, -120 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Dect");
        switchDirection = !switchDirection;
        foreach (SwitchButtonObjects objects in objectsToTrigger)
        {
            objects.TriggerChanged(switchDirection);
        }
    }
}
