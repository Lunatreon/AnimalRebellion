using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * manage the hock apperence and movement 
 * it's used by the squeezing mashine
 */
public class Hock : MonoBehaviour
{
    [Tooltip("positions the hock should pass through")]
    [SerializeField]
    public Vector3[] positions;

    [Tooltip("where the hock start in the position array")]
    [SerializeField]
    private int positionToUse = 1;

    //to signal all hock if they should move or not
    private static bool shouldMove = true;

    [Tooltip("the gameobject of the animal which is alive")]
    [SerializeField]
    private GameObject chickenAlive;
    [Tooltip("Gameobject of the output after the animal is hit by a mashine")]
    [SerializeField]
    private GameObject chickenDead;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldMove)
        {
            //calculate the movement of the hock
            Vector3 movement = (positions[positionToUse] - positions[positionToUse - 1]);
            float resultOfDot = Vector3.Dot(movement, (positions[positionToUse] - transform.position));

            //if the object is near the next position array reset it to the position and set the next position where it should move
            if (movement.magnitude < 0.2f || resultOfDot < 0)
            {
                transform.position = positions[positionToUse];
                positionToUse += 1;
                //if the array is finished reset the complete process and "respawn" the hock at the first array position
                if (positionToUse == positions.Length)
                {
                    transform.position = positions[0];
                    positionToUse = 1;
                    chickenDead.SetActive(false); 
                    chickenAlive.SetActive(true);
                }
            }
            else
            {
                transform.position = transform.position + movement.normalized * Time.fixedDeltaTime / 2;
            }
        }
    }

    /*
     * change the apperance of the object on the hock, if it is hit
     */
    public void isHitBy()
    {
        chickenDead.SetActive(true); 
        chickenAlive.SetActive(false);
    }

    /*
     * change the static variable values to turn off the mashine
     */
    public static void changeMovement(bool input)
    {
        shouldMove = input;
    }
}
