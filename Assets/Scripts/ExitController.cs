using UnityEngine;
using TMPro;

public class ExitController : MonoBehaviour
{
    public TMP_Text resultText;

    void Start()
    {
        string message = PlayerPrefs.GetString(
            "ResultMessage",
            "Game Over"
        );

        string playerName = PlayerPrefs.GetString(
            "FinalPlayerName",
            "Player"
        );

        int level = PlayerPrefs.GetInt("FinalLevel", 1);
        int score = PlayerPrefs.GetInt("FinalScore", 0);
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);

        resultText.text =
            message + "\n" +
            "Player: " + playerName + "\n" +
            "Level: " + level +
            "   Score: " + score +
            "   Time: " + finalTime.ToString("F1");
    }
}