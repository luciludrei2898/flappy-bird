using UnityEngine;

public class Column : MonoBehaviour
{
    // Method executed when a trigger collider enters the 2D collider
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the object colliding has the "Player"

        if (collider.CompareTag("Player"))
        {
            // Call the AddPoints method 
            GameController.instance.AddPoints();
        }

    }

}
