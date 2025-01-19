using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // METODO SALIR JUEGO
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // COMPILACION CIERRA
        Application.Quit();
#endif
    }

    // METODO REINICIAR

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
