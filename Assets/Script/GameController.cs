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

        if (!PlayerPrefs.HasKey("FirstTime"))  
        {
            deathCount = 0;
            PlayerPrefs.SetInt("DeathCount", deathCount); 
            PlayerPrefs.SetInt("FirstTime", 1);  
            PlayerPrefs.Save();
        }
        else
        {
            deathCount = PlayerPrefs.GetInt("DeathCount", 0); 
        }

        Debug.Log("Contador de muertes cargado: " + deathCount);
    }

    void Start()
    {
        gameOverText.SetActive(false);
        buttonExit.SetActive(false);
        buttonPlayAgain.SetActive(false);
    }

    public void AddPoints()
    {
        if (gameOver) return;

        score++;
        scoreText.text = "Score: " + score;
        SoundSystem.instancie.PlayPoint();
    }

    public void BirdDie()
    {
        deathCount++;
        Debug.Log("Muertes del pájaro: " + deathCount);

        PlayerPrefs.SetInt("DeathCount", deathCount);
        PlayerPrefs.Save();

        gameOverText.SetActive(true);
        buttonExit.SetActive(true);
        buttonPlayAgain.SetActive(true);
        gameOver = true;

        if (deathCount % 3 == 0) 
        {
            if (ControllerAds.instance != null)
            {
                ControllerAds.instance.ShowAd();
            }
            else
            {
                Debug.LogError("ControllerAds no está inicializado.");
            }
        }
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

