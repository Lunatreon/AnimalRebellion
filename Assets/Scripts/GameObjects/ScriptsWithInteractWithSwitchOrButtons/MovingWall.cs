using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A wall that could be go up and down if a event is triggered
 */
public class MovingWall : SwitchButtonObjects
{
    //Movement Vector to give the Wall a direction
    private Vector3 Movement = new Vector3(0, 2, 0);

    //boolean to controll the direction and if it should move;
    private bool direction = false;
    private bool shouldMove = false;
    public override void TriggerChanged(bool switchInput)
    {
        direction = switchInput;
        shouldMove = true;
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            //Move Down
            if (direction)
            {
                this.gameObject.transform.position = this.gameObject.transform.position - this.Movement * Time.deltaTime;
                if (gameObject.transform.position.y < -0.5)
                {
                    double correct = gameObject.transform.position.y + 0.5;
                    gameObject.transform.position = gameObject.transform.position - new Vector3(0, (float)correct, 0);
                    shouldMove = false;
                }
            }
            //Move Up
            else
            {
                this.gameObject.transform.position = this.gameObject.transform.position + this.Movement * Time.deltaTime;
                if (gameObject.transform.position.y > 0.5)
                {
                    double correct = gameObject.transform.position.y - 0.5;
                    gameObject.transform.position = gameObject.transform.position - new Vector3(0, (float)correct, 0);
                    shouldMove = false;
                }
            }
        }
    }
}

