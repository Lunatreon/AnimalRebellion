using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hock : MonoBehaviour
{

    [SerializeField]
    public Vector3[] positions;

    [SerializeField]
    private int positionToUse = 1;

    [SerializeField]
    private static bool shouldMove = true;

    [SerializeField]
    private GameObject chickenAlive;
    [SerializeField]
    private GameObject chickenDead;

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            Vector3 movement = (positions[positionToUse] - positions[positionToUse - 1]);
            float resultOfDot = Vector3.Dot(movement, (positions[positionToUse] - transform.position));

            if (movement.magnitude < 0.2f || resultOfDot < 0)
            {
                transform.position = positions[positionToUse];
                positionToUse += 1;
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
                transform.position = transform.position + movement.normalized * Time.deltaTime / 2;
            }
        }
    }

    public void isHitBy()
    {
        chickenDead.SetActive(true); 
        chickenAlive.SetActive(false);
    }


    public static void changeMovement(bool input)
    {
        shouldMove = input;
    }
}
