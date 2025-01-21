using UnityEngine;

public class Pajaro : MonoBehaviour
{
    // ATTRIBUTES

    private bool isDead = false; // Flag to check if the bird is dead
    private Rigidbody2D rb2d; // The bird's Rigidbody2D component for physics
    private Animator anim; // The Animator component to control bird animations
    public float upForce = 200f; // The force applied to make the bird fly

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
#if UNITY_ANDROID || UNITY_IPhonePlayer
            // Check if the screen is touched, and if the first touch has just begun
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Fly();
                Debug.Log("Toque");
            }
#elif UNITY_STANDALONE || UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Fly(); 
            }
#endif
        }
    }

    // Function to make the bird fly
    void Fly()
    {
        rb2d.linearVelocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * upForce);
        anim.SetTrigger("Flap");
        SoundSystem.instancie.PlayFly();
    }

    // This method is called when the bird collides with another object 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true; 
        anim.SetTrigger("Die");
        GameController.instance.BirdDie();
        rb2d.linearVelocity = Vector2.zero;
        SoundSystem.instancie.PlayDie();
    }
}
