using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private Rigidbody2D rb2d;  

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    // Start 
    void Start()
    {
        // Set the initial linear velocity to make the object move 
        rb2d.linearVelocity = new Vector2(GameController.instance.scrollSpeed, 0);
    }

    // Update 
    void Update()
    {
        // If the game is over, stop the object's 
        if (GameController.instance.gameOver)
        {
            rb2d.linearVelocity = Vector2.zero;  // Stop
        }
    }
}

