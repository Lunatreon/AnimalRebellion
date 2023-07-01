using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public Camera cam;
    private Rigidbody myRigibody;

    private bool air;

    private float sphereRadius = 1;
    private float raycastOffset = 0.1f;

    private Vector3 force;


    private Vector3 spawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigibody = GetComponent<Rigidbody>();
        //shipsBone = animator.GetBoneTransform(HumanBodyBones.Hips);
    }

    private void Update()
    {
        force = Vector3.zero;

        force.x = Input.GetAxis("Horizontal");
        force.z = Input.GetAxis("Vertical");

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        { 
            force *= 100;
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
        }
        else
        {
           force *= 50;
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }

        RaycastHit hit;
        air = Physics.SphereCast(transform.position + Vector3.up * (sphereRadius + Physics.defaultContactOffset),
            sphereRadius - Physics.defaultContactOffset
            , Vector3.down, out hit, raycastOffset);
        //animator.SetBool("Grounded", air);


        //evtl noch die physikalischen kollisionen mit einbeziehen
        if (force.magnitude != 0)
        {
            Vector3 actForce = new Vector3(0, myRigibody.velocity.y, 0);
            myRigibody.velocity = actForce;
            force = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * force;
            myRigibody.AddForce(force);
            transform.rotation = Quaternion.LookRotation(force);
            //animator.SetFloat("Speed", animatonSpeed);
            force = new Vector3(0, 0, 0);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Key"))
        {
            collision.gameObject.SetActive(false);
            GameManagerSlaugtherhouse.PublicGameManager.playerGotKey();
        }else if (collision.gameObject.tag.Equals("Target"))
        {
            GameManagerSlaugtherhouse.PublicGameManager.playerGotChickenMother();
        }
    }
}
