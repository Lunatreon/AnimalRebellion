using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/*
 * Squeezer logic to manage to "make" animals into meat, without an animator.
 */
public class Squeezer : SwitchButtonObjects
{
    [SerializeField]
    private GameObject SqueezerLeft;
    private Vector3 startPosLeft;

    [SerializeField]
    private GameObject SqueezerRight;
    private Vector3 startPosRight;

    [Tooltip("Object which could be turned on and off to give the information to someone that it stopped")]
    [SerializeField]
    private GameObject eventObject;

    //boolean values to manage diffrent stages (booleans because two variables could be true at one time)
    private bool shouldSqueeze = false;
    private bool shouldReturnToPosition = false;
    private bool shouldRun = true;

    private Hock animalInThisMashine;

    [Tooltip("All visual effects which should play after the mashine hit something")]
    [SerializeField]
    private VisualEffect[] bloodEffectForSqueezer;


    /*
     * Turn the mashine off and on with the switch input
     */
    public override void TriggerChanged(bool switchInput)
    {
        shouldRun = !switchInput;
        eventObject.SetActive(switchInput);

        if (!shouldReturnToPosition && !shouldSqueeze)
        {
            Hock.changeMovement(shouldRun);

        }
    }

    /*
     * Start is called before the first frame update
     */
    void Start()
    {
        startPosLeft = SqueezerLeft.transform.position;
        startPosRight = SqueezerRight.transform.position;

        eventObject.SetActive(false);
    }

    /*
     * Update is called once per frame
     */
    void FixedUpdate()
    {
        if (shouldRun)
        {
            if (shouldSqueeze)
            {
                //check if both squeezer are near enoph or if the was to slow, both vectors will show in opposit directions and it should also stop ( happend in Debuging sometimes if i paused the game in the wrong time xd) 
                if ((SqueezerLeft.transform.position - SqueezerRight.transform.position).magnitude < 0.1 || Vector3.Dot(SqueezerLeft.transform.position - SqueezerRight.transform.position, startPosLeft - startPosRight) < 0)
                {
                    shouldReturnToPosition = true;
                    shouldSqueeze = false;
                    
                    //Turn object into something else after both squeezer "hit" each other
                    if (animalInThisMashine != null)
                    {
                        animalInThisMashine.isHitBy();
                        animalInThisMashine = null;
                    }

                    foreach(VisualEffect effect in bloodEffectForSqueezer)
                    {
                        effect.Play();
                    }
                }
                else
                {
                    //Move both squeezer in the center
                    Vector3 movement = startPosLeft - startPosRight;

                    SqueezerLeft.transform.position = SqueezerLeft.transform.position - movement * Time.fixedDeltaTime * 3;
                    SqueezerRight.transform.position = SqueezerRight.transform.position + movement * Time.fixedDeltaTime * 3;
                }
            }
        }
        //return to the start position of the squeezers
        if (shouldReturnToPosition)
        {
            Vector3 movementLeft = startPosLeft - SqueezerLeft.transform.position;
            SqueezerLeft.transform.position = SqueezerLeft.transform.position + movementLeft * Time.fixedDeltaTime * 2;

            Vector3 movementRight = startPosRight - SqueezerRight.transform.position;
            SqueezerRight.transform.position = SqueezerRight.transform.position + movementRight * Time.fixedDeltaTime * 2;

            //start the miles that the next animal will get in the mashine
            if (movementRight.magnitude < 0.3 && movementLeft.magnitude < 0.3)
            {
                if(shouldRun)
                    Hock.changeMovement(true);
            }
            else if (movementRight.magnitude < 0.01 && movementLeft.magnitude < 0.01)
            {
                shouldReturnToPosition = false;
            }
        }

    }

    /*
     * check if a animal is in the mashine to start the smashing process
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("AnimalInSlaugtherhouse"))
        {
            Hock.changeMovement(false);
            shouldSqueeze = true;

            animalInThisMashine = other.GetComponent<Hock>();
        }
    }

    /*
     * Check if a guard is in the mashine while it Squeeze
     */
    private void OnTriggerStay(Collider other)
    {
        if (shouldSqueeze && shouldRun)
        {
            if (other.tag.Equals("Guard"))
            {
                other.gameObject.SetActive(false);
            }

        }
    }


}
