using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
