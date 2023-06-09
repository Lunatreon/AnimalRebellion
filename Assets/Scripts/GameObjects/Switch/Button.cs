using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private List<SwitchButtonObjects> objectsToTrigger;

    private int objectsOnButton = 0;
    private void OnTriggerEnter(Collider other)
    {
        objectsOnButton++;
        if(objectsOnButton == 1)
        {
            foreach (SwitchButtonObjects objects in objectsToTrigger)
            {
                objects.TriggerChanged(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsOnButton--;
        if(objectsOnButton == 0)
        {
            foreach(SwitchButtonObjects objects in objectsToTrigger)
            {
                objects.TriggerChanged(false);
            }
        }
    }
}
