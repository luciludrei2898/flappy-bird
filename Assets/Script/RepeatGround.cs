using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundHorizontalLenght;
    private float sizeGround = 19.86f;

    private void Awake()
    {
        groundCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundHorizontalLenght = groundCollider.size.x;
    }

    private void repeatBackGround()
    {
        transform.Translate(Vector2.right * sizeGround * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -sizeGround)
        {
            repeatBackGround();
        }
    }
}
