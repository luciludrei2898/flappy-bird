using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Advertisements;  // Asegúrate de tener este using

public class GameController : MonoBehaviour, IUnityAdsListener
{
    public static GameController instance;

    public GameObject gameOverText;
    public GameObject buttonExit;
    public GameObject buttonPlayAgain;
    public bool gameOver;
    public float scrollSpeed = -1.5f;

    private int score = 0;
    public TMP_Text scoreText;

    // Variables para controlar los anuncios
    private int deathCount = 0;  // Contador de muertes del pájaro
    public int deathsToShowAd = 3;  // Cantidad de muertes necesarias para mostrar el anuncio
    private string adUnitId = "Rewarded_Android";  // ID del anuncio (asegúrate de usar el correcto)

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
        // Inicializa Unity Ads
        Advertisement.Initialize("your_game_id", true);  
        Advertisement.AddListener(this); 
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

        if (deathCount >= deathsToShowAd)
        {
            ShowAd();
            deathCount = 0; 
        }
    }

    private void ShowAd()
    {
        
            Advertisement.Show(adUnitId);
        
       
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

    // RESTART
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


    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Error en Unity Ads: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == adUnitId)
        {
            if (showResult == ShowResult.Finished)
            {
                Debug.Log("El anuncio ha sido completado.");
            }
            else if (showResult == ShowResult.Skipped)
            {
                Debug.Log("El anuncio fue saltado.");
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogError("El anuncio falló.");
            }
        }
    }
}

internal interface IUnityAdsListener
{
}