using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class sawBladeMashine : SwitchButtonObjects
{
    // Start is called before the first frame update
    [SerializeField]
    private Vector3 spawnPoint;
    [SerializeField]
    private VisualEffect bloodEffect;
    private List<SwitchButtonObjects> objectsToTriggerOnEvent;
    void Start()
    {
        bloodEffect.startSeed = 1;
        objectsToTriggerOnEvent = new List<SwitchButtonObjects>(this.gameObject.GetComponentsInChildren<SwitchButtonObjects>());
        objectsToTriggerOnEvent.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("chickenThreadmile"))
        {
            collision.gameObject.transform.position = spawnPoint;
            collision.gameObject.transform.rotation = new Quaternion(0,0,0,0);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3();
        } else if (collision.gameObject.tag.Equals("box"))
        {
            TriggerChanged(true);
        }
    }

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
