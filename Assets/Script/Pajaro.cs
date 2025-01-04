using UnityEngine;

public class Pajaro : MonoBehaviour
{

    // ATRIBUTOS

    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;
    public float upForce = 200f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // EN EL CASO DE ESTAR MUERTO, NO SE MUEVE
        if (!isDead)
        {
#if UNITY_ANDROID || UNITY_IPhonePlayer
                // En dispositivos móviles (Android o iOS), detecta el toque
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Fly();
                }
#elif UNITY_STANDALONE || UNITY_EDITOR
            // En ordenadores (Windows, Mac, Linux, o en el editor de Unity), detecta el clic del ratón
            if (Input.GetMouseButtonDown(0))
            {
                Fly();
            }
#endif
        }
    }

    // Función para hacer que el pájaro vuele
    void Fly()
    {
        rb2d.linearVelocity = Vector2.zero;
        rb2d.AddForce(Vector2.up * upForce);
        anim.SetTrigger("Flap");
        SoundSystem.instancie.PlayFly();
    }

    // METODO QUE MIDE SI EL PAJARO HA COLISIONADO CON OTRO OBJETO, EN ESTE CASO MUERE
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        anim.SetTrigger("Die");
       GameController.instance.BirdDie();
        rb2d.linearVelocity = Vector2.zero;
        SoundSystem.instancie.PlayDie();
    }
}
