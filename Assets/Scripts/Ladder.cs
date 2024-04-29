using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the player's gravity scale to 0 to simulate climbing
            other.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the player's gravity scale when leaving the ladder trigger
            other.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }
}
