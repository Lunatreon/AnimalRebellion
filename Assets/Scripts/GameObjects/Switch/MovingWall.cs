using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : SwitchButtonObjects
{
    private Vector3 Movement = new Vector3(0, 2, 0);
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

