using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour
{
    
    private Animator animator;
    public Camera cam;
    private Rigidbody myRigibody;

    //manage the input 
    private Vector3 force;
    
    // Start is called before the first frame update
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

            force = 100 * force.normalized;
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
        }
        else
        {
           force = 50 * force.normalized;
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }

        //using the force of unity to interact better with physic objects
        if (force.magnitude != 0)
        {
            Vector3 actForce = new Vector3(0, myRigibody.velocity.y, 0);
            myRigibody.velocity = actForce;
            force = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * force;
            myRigibody.AddForce(force);
            transform.rotation = Quaternion.LookRotation(force);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

    /*
     * check which object is collided with our chicken :)
     */
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
