using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d.linearVelocity = new Vector2(GameController.instance.scrollSpeed,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameOver)
        {
            rb2d.linearVelocity = Vector2.zero;
        }
    }
}
