using UnityEngine;
using UnityEngine.Rendering;

public class ColumnPool : MonoBehaviour
{
    // ATTRIBUTES

    public int columnPoolSize = 6;  
    public GameObject columnPrefab; 

    private GameObject[] columns;  
    private Vector2 objectPoolPosition = new Vector2(-14, 0);  
    private float timeLastColumn;  
    public float resetRate;  // Rate at which new columns are created

    // COLUMN PARAMETERS

    public float columnMin = -2.9f;  // Minimum Y position 
    public float columnMax = 1.4f;  // Maximum Y position 
    private float positionXColumn = 10f;  // X position for the columns

    private int currentColumn;  

    void Start()
    {
        columns = new GameObject[columnPoolSize];  // Initialize the columns array

        // Instantiate columns
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }

        ColumnApatirion();  // Create the first column
    }

    // Update 
    void Update()
    {
        timeLastColumn += Time.deltaTime;  // Increment the timer

        // If the timer exceeds the reset rate and the game isn't over, function new columnn
        if (timeLastColumn >= resetRate && !GameController.instance.gameOver)
        {
            timeLastColumn = 0;  // Reset 
            ColumnApatirion();  // Call the function
        }
    }

    // Method to spawn a new column
    void ColumnApatirion()
    {
        // Generate a random Y position
        float positionY = Random.Range(columnMin, columnMax);

        // Set the position of the current column
        columns[currentColumn].transform.position = new Vector2(positionXColumn, positionY);

        currentColumn++;  // Move to the next column 

        // Reset it to 0 
        if (currentColumn >= columnPoolSize)
        {
            currentColumn = 0;
        }
    }
}
