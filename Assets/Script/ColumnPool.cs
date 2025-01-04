using UnityEngine;
using UnityEngine.Rendering;

public class ColumnPool : MonoBehaviour
{

    // ATRIBUTOS

    public int columnPoolSize = 6;
    public GameObject columnPrefab;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-14, 0);

    private float timeLastColumn;
    public float resetRate;

    // VALORES COLUMNAS 

    public float columnMin = -2.9f;
    public float columnMax = 1.4f;
    private float positionXColumn = 10f;

    private int currentColumn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        columns = new GameObject[columnPoolSize];

        for(int i= 0; i<columnPoolSize; i++)
        {
            columns[i] = Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }

        ColumnApatirion();
    }

    // Update is called once per frame
    void Update()
    {
        timeLastColumn += Time.deltaTime; 

        if (timeLastColumn >= resetRate && !GameController.instance.gameOver)
        {
            timeLastColumn = 0; // RESET AL ACUMULADOR DE TIEMPO

            ColumnApatirion();

        }
    }

    void ColumnApatirion()
    {
        // Columnas con valores aleatorios

        float positionY = Random.Range(columnMin, columnMax);

        // Le damos la posicion a la columna del array que hemos creado antes
        columns[currentColumn].transform.position = new Vector2(positionXColumn, positionY);
        currentColumn++; // Asi pasamos a todas los columnas

        // Para evitar salirnos del array, comprobamos la columna que elegimos y la ponemos a cero si es menor que el size

        if (currentColumn >= columnPoolSize)
        {
            currentColumn = 0;
        }
    }

}
