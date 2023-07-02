using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script which will only "delete" animals which reached the exit (it's a trigger Boxcolider)
 */
public class outsideFence : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("AnimalInSlaugtherhouse"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
