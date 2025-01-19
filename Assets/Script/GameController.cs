using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public GameObject gameOverText;
    public GameObject buttonExit;
    public GameObject buttonPlayAgain;
    public bool gameOver;
    public float scrollSpeed = -1.5f;

    private int score = 0;
    public TMP_Text scoreText;

    void Awake()
    {
        if (GameController.instance == null)
        {
        GameController.instance = this;
        } else if (GameController.instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void AddPoints()
    {
        if (gameOver) return;

        score++;
        scoreText.text = "Score: " + score;

        SoundSystem.instancie.PlayPoint();
    }

    // METODO QUE MUESTRA EL TEXT DE GAME OVER

    public void BirdDie()
    {
        // CUANDO ESTA MUERTO, SE ACTIVA EL TEXTO EN TRUE Y SE MUESTRA EN PANTALLA
        gameOverText.SetActive(true);
        buttonExit.SetActive(true);
        buttonPlayAgain.SetActive(true);

        gameOver = true;
    }

    public void OnDestroy()
    {
        if(GameController.instance == this)
        {
            GameController.instance = null;
        }
    }
}
