using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenPreferences()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("preferences");
    }
}