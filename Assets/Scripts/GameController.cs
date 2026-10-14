using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TMP_Text playerNameText;
    public TMP_Text levelText;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject pauseText;

    private float elapsedTime;
    private int score;
    private int level;
    private bool isPaused;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        elapsedTime = 0f;
        score = 0;
        level = 1;
        isPaused = false;

        pauseText.SetActive(false);

        string playerName =
            PlayerPrefs.GetString("PlayerName", "Andy");

        playerNameText.text = "Player: " + playerName;
        UpdateDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;

            timerText.text =
                "Time: " + elapsedTime.ToString("F1");
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseText.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        level = 1 + (score / 50);

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        levelText.text = "Level: " + level;
        scoreText.text = "Score: " + score;
    }
    public void ExitGame()
    {
        EndGame("Game ended early.");
    }

    public void EndGame(string message)
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetString("ResultMessage", message);
        PlayerPrefs.SetString(
            "FinalPlayerName",
            PlayerPrefs.GetString("PlayerName", "Andy")
        );
        PlayerPrefs.SetInt("FinalLevel", level);
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.SetFloat("FinalTime", elapsedTime);
        PlayerPrefs.Save();

        SceneManager.LoadScene("exit");
    }
}