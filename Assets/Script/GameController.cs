using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Advertisements;  // Asegúrate de tener este using

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

    private int deathCount = 0;  
    public int deathsToShowAd = 3;  

    void Awake()
    {
        if (GameController.instance == null)
        {
            GameController.instance = this;
        }
        else if (GameController.instance != this)
        {
            Destroy(gameObject);
        }
    }

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
        deathCount++;
        Debug.Log("Muertes del pájaro: " + deathCount);

        gameOverText.SetActive(true);
        buttonExit.SetActive(true);
        buttonPlayAgain.SetActive(true);
        gameOver = true;

    }

    // EXIT GAME
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // COMPILACION CIERRA
        Application.Quit();
#endif
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }


 
}

