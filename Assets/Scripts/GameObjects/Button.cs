using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Button which need a object on it to be pushed. In this case only the player or a chest could do that :)
 * The object in the list will be informed if it changed ^^
 */
public class Button : MonoBehaviour
{
    [Tooltip("all object which are effect by the button")]
    [SerializeField]
    private List<SwitchButtonObjects> objectsToTrigger;

    //know how much objects are on the button.
    private int objectsOnButton = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Chest"))
        {
            objectsOnButton++;
            if (objectsOnButton == 1)
            {
                foreach (SwitchButtonObjects objects in objectsToTrigger)
                {
                    objects.TriggerChanged(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Chest"))
        {
            objectsOnButton--;
            if (objectsOnButton == 0)
            {
                foreach (SwitchButtonObjects objects in objectsToTrigger)
                {
                    objects.TriggerChanged(false);
                }
            }
        }
    }
}
