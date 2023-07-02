using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * if the collider of the object trigger with the player, the player got caught and the game over screen should appear
 */
public class CatchPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameManagerSlaugtherhouse.PublicGameManager.playerGotCaught();
        }
    }
}
