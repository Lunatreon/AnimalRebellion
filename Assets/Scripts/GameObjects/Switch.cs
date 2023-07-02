using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Switch object, which could be turned on and off by the player
 */
public class Switch : MonoBehaviour
{
    [Tooltip("Objects which are effected by the switch")]
    [SerializeField]
    private List<SwitchButtonObjects> objectsToTrigger;

    public GameObject switchAnimation;

    private bool switchDirection = false;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        //"animation" of the switch (turn it in two direction)
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

    /*
     * if the player hit the button activate the process of moving and give the information to the objects
     */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        switchDirection = !switchDirection;
        foreach (SwitchButtonObjects objects in objectsToTrigger)
        {
            objects.TriggerChanged(switchDirection);
        }
    }
}
