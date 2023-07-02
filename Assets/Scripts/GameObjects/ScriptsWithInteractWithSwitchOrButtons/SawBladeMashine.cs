using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/*
 * the complect mashine which will turn animals into sausage
 * it will manage the spawn and respawn algorithem of the complex
 * have all object which could be turned of by an event
 */
public class SawBladeMashine : SwitchButtonObjects
{
    [Tooltip("the spawn point where the object should respawn if they fall into the mashine")]
    [SerializeField]
    private Vector3 spawnPointForInput;

    [Tooltip("blood effect which should be played if the mashine is running")]
    [SerializeField]
    private VisualEffect bloodEffect;

    [Tooltip("smoke effect which should be played after the mashine is destroyed")]
    [SerializeField]
    private VisualEffect smokeEffect;

    //List of all object which can turned off and on. They are all childs of the Gameobject
    private List<SwitchButtonObjects> objectsToTriggerOnEvent;

    [Tooltip("the output of the mashine")]
    [SerializeField]
    private GameObject output;
    [Tooltip("where the output should spawn")]
    [SerializeField]
    private Vector3 spawnPointForOutput;

    [Tooltip("key which would be spawned after the mashine is destroyes")]
    [SerializeField]
    private GameObject keyToThrew;
    [Tooltip("the force to throw the key away")]
    [SerializeField]
    private Vector3 forceDirection;

    /*
     * create the list of Objects connectet with the mashine (their child objects with SwitchButtonObjects childs)
     */
    void Start()
    {
        objectsToTriggerOnEvent = new List<SwitchButtonObjects>(this.gameObject.GetComponentsInChildren<SwitchButtonObjects>());
        objectsToTriggerOnEvent.Remove(this);

        smokeEffect.Stop();
        bloodEffect.Play();

        keyToThrew.SetActive(false);
    }

    /*
     * The other collison box do delete the output of the mashine when they are not visible anymore (and hit the customized area)
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Food"))
            Destroy(other.gameObject);
    }

    /*
     * reset the input object to the start location and spawn a output object
     * + the stop if the collision is with on box
     */
    private void OnCollisionEnter(Collision collision)
    {
        //if the mashine is hit by a animal(respawn it and spawn an output object
        if (collision.gameObject.tag.Equals("AnimalInSlaugtherhouse"))
        {
            collision.gameObject.transform.position = spawnPointForInput;
            collision.gameObject.transform.rotation = new Quaternion(0,0,0,0);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3();

       
            GameObject obj = Instantiate(output, spawnPointForOutput, Quaternion.identity);
            obj.transform.parent = this.gameObject.transform;
            obj.SetActive(true);
        }
        //if the mashine is bit by a chest stop the process and activate the key
        else if (collision.gameObject.tag.Equals("Chest"))
        {
            smokeEffect.Play();
            TriggerChanged(true);
            collision.gameObject.SetActive(false);
            keyToThrew.SetActive(true);
            keyToThrew.GetComponent<Rigidbody>().AddForce(forceDirection);
        } 
        //if the player it hit, go to the game over screen
        else if (collision.gameObject.tag.Equals("Player"))
        {
            GameManagerSlaugtherhouse.PublicGameManager.playerGotCaught();
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
