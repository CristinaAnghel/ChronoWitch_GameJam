using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the next scene in the build index (e.g. your game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Optional: helps test in Editor
        Application.Quit();
    }

    public void ReturnToMain()
    {
        // Loads the next scene in the build index (e.g. your game)
        SceneManager.LoadScene("Menu");
    }
}
