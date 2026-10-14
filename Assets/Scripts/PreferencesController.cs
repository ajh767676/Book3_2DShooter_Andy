using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PreferencesController : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Dropdown enemySpeedDropdown;
    public Slider spawnRateSlider;
    public TMP_Text spawnRateValueText;

    void Start()
    {
        spawnRateSlider.value = 8;
        UpdateSpawnRateText(spawnRateSlider.value);
    }

    public void UpdateSpawnRateText(float value)
    {
        int enemiesPerMinute = Mathf.RoundToInt(value);

        spawnRateValueText.text =
            enemiesPerMinute + " enemies/min";
    }

    public void StartGame()
    {
        string playerName = playerNameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }

        PlayerPrefs.SetString("PlayerName", playerName);

        PlayerPrefs.SetInt(
            "EnemySpeed",
            enemySpeedDropdown.value
        );

        PlayerPrefs.SetInt(
            "EnemiesPerMinute",
            Mathf.RoundToInt(spawnRateSlider.value)
        );

        PlayerPrefs.Save();
        SceneManager.LoadScene("game");
    }
}