using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Blades of a mashine
 * the script will only rotate it but stopped if a event triggers
 */
public class SawBlade : SwitchButtonObjects
{
    private bool shouldRotate = true;

    public override void TriggerChanged(bool switchInput)
    {
        shouldRotate = !switchInput;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldRotate)
            this.gameObject.transform.Rotate(0, 0, 0.5f);
    }
}
