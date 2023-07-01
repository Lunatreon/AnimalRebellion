using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Squeezer : SwitchButtonObjects
{
    [SerializeField]
    private GameObject SqueezerLeft;
    private Vector3 startPosLeft;

    [SerializeField]
    private GameObject SqueezerRight;
    private Vector3 startPosRight;

    [SerializeField]
    private GameObject eventObject;

    private bool shouldSqueeze = false;

    private bool shouldReturnToPosition = false;

    private bool shouldRun = true;

    private hock animalInThisMashine;

    [SerializeField]
    private VisualEffect[] bloodEffectForSqueezer;



    public override void TriggerChanged(bool switchInput)
    {
        shouldRun = !switchInput;
        eventObject.SetActive(switchInput);

        if (!shouldReturnToPosition && !shouldSqueeze)
        {
            hock.changeMovement(shouldRun);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosLeft = SqueezerLeft.transform.position;
        startPosRight = SqueezerRight.transform.position;

        eventObject.SetActive(false);
    }

    // Update is called once per frame
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
                    Vector3 movement = startPosLeft - startPosRight;

                    SqueezerLeft.transform.position = SqueezerLeft.transform.position - movement * Time.fixedDeltaTime * 3;
                    SqueezerRight.transform.position = SqueezerRight.transform.position + movement * Time.fixedDeltaTime * 3;
                }
            }
        }
        if (shouldReturnToPosition)
        {
            Vector3 movementLeft = startPosLeft - SqueezerLeft.transform.position;
            SqueezerLeft.transform.position = SqueezerLeft.transform.position + movementLeft * Time.fixedDeltaTime * 2;

            Vector3 movementRight = startPosRight - SqueezerRight.transform.position;
            SqueezerRight.transform.position = SqueezerRight.transform.position + movementRight * Time.fixedDeltaTime * 2;

            if (movementRight.magnitude < 0.3 && movementLeft.magnitude < 0.3)
            {
                if(shouldRun)
                    hock.changeMovement(true);
            }
            else if (movementRight.magnitude < 0.01 && movementLeft.magnitude < 0.01)
            {
                shouldReturnToPosition = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("chickenThreadmile"))
        {
            hock.changeMovement(false);
            shouldSqueeze = true;

            animalInThisMashine = other.GetComponent<hock>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (shouldSqueeze)
        {
            if (other.tag.Equals("Guard"))
            {
                other.gameObject.SetActive(false);
            }

        }
    }


}
