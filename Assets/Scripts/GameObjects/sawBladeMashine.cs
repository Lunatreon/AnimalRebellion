using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class sawBladeMashine : SwitchButtonObjects
{
    // Start is called before the first frame update
    [SerializeField]
    private Vector3 spawnPointForInput;
    [SerializeField]
    private VisualEffect bloodEffect;
    [SerializeField]
    private VisualEffect smokeEffect;

    private List<SwitchButtonObjects> objectsToTriggerOnEvent;

    [SerializeField]
    private GameObject output;
    [SerializeField]
    private Vector3 spawnPointForOutput;


    /*
     * create the list of Objects connectet with the mashine (their child objects with SwitchButtonObjects childs)
     */
    void Start()
    {
        objectsToTriggerOnEvent = new List<SwitchButtonObjects>(this.gameObject.GetComponentsInChildren<SwitchButtonObjects>());

        objectsToTriggerOnEvent.Remove(this);

        smokeEffect.Stop();
        bloodEffect.Play();
    }

    /*
     * The other collison box do delete the output of the mashine when they are not visible anymore (and hit sthe customized area)
     */
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    /*
     * reset the input object to the start location and spawn a output object
     * + the stop if the collision is with on box
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("chickenThreadmile"))
        {
            collision.gameObject.transform.position = spawnPointForInput;
            collision.gameObject.transform.rotation = new Quaternion(0,0,0,0);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3();

       
            GameObject obj = Instantiate(output, spawnPointForOutput, Quaternion.identity);
            obj.SetActive(true);

        } else if (collision.gameObject.tag.Equals("box"))
        {
            smokeEffect.Play();
            TriggerChanged(true);
        }
    }

    /*
     * turn the complete mashine on or off
     */
    public override void TriggerChanged(bool switchInput)
    {
        foreach(SwitchButtonObjects obj in objectsToTriggerOnEvent)
        {
            obj.TriggerChanged(switchInput);
        }
        if (switchInput)
        {
            bloodEffect.Stop();
        }
        else
        {
            bloodEffect.Play();
        }
    }
}
