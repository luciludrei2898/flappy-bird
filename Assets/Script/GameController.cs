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
    private bool hasDiedOnce = false;  
    private int randomDeathThreshold = 0;  

    // Called when the script is first loaded
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

        Debug.Log("Loaded death count: " + deathCount);
    }

    // Called at the start of the game and Hide UI elements when the game starts
    void Start()
    {
        // Check if we are in the correct scene
        if (SceneManager.GetActiveScene().name == "SampleScene") // Replace with your actual scene name
        {
            gameOverText.SetActive(false);
            buttonExit.SetActive(false);
            buttonPlayAgain.SetActive(false);
        }
    }


    // Called to increment the score when the bird successfully passes an obstacle
    public void AddPoints()
    {
        if (gameOver) return;  // Do nothing if the game is over

        score++;  // Increase score +1
        scoreText.text = "Score: " + score;  // Update score display
        SoundSystem.instancie.PlayPoint();  // Play the sound
    }

    // Called when the bird dies (collides with an obstacle or falls)
    public void BirdDie()
    {
        if (!hasDiedOnce)  // Only increment death count if the bird hasn't died before in this session
        {
            deathCount++;  
            PlayerPrefs.SetInt("DeathCount", deathCount);  // Save updated death count in PlayerPrefs
            PlayerPrefs.Save();  // Save changes
            hasDiedOnce = true;  // Mark that the bird 
            randomDeathThreshold = Random.Range(3, 6);  // Set a random threshold between 3 and 5 deaths for showing the ad
            Debug.Log("Bird deaths: " + deathCount);
        }

        // Show the game over UI elements
        if (SceneManager.GetActiveScene().name == "SampleScene") // Replace with your actual scene name
        {
            gameOverText.SetActive(true);
            buttonExit.SetActive(true);
            buttonPlayAgain.SetActive(true);
        }
        gameOver = true;  // Mark the game as over

        if (deathCount >= randomDeathThreshold)
        {
            if (ControllerAds.instance != null)
            {
                ControllerAds.instance.ShowAd();  // Show the ad
            }
            else
            {
                Debug.LogError("ControllerAds is not initialized.");
            }
            PlayerPrefs.SetInt("DeathCount", deathCount);  // Reset the death count 
            PlayerPrefs.Save();  // Save the reset 
        }
    }

    // Method to quit the game
    public void QuitApplication()
    {
        // Reset death count 
        PlayerPrefs.SetInt("DeathCount", 0);  // Set 0
        PlayerPrefs.Save();  // Save the changes 

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop play mode in Unity Editor
#else
        // Quit the application in a built game
        Application.Quit();
#endif
    }

    // Method to restart the game by reloading the current scene
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    // Method to go to the next level or scene
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    // Method to load the main menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");  
    }

    // Method to load the credits scene
    public void Credits()
    {
        SceneManager.LoadScene("Credits"); 
    }
}



