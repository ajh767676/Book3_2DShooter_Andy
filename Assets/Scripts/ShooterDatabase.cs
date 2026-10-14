using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ShooterDatabase : MonoBehaviour
{
    public TMP_Text highScoresText;

    private const string saveUrl =
        "http://104.129.130.168/updateShooters.php";

    private const string scoresUrl =
        "http://104.129.130.168/getShooters.php";

    private class ShooterScore
    {
        public string playerName;
        public int level;
        public int score;
        public float time;
    }

    IEnumerator Start()
    {
        highScoresText.text = "Saving score...";

        yield return SaveScore();
        yield return LoadScores();
    }

    IEnumerator SaveScore()
    {
        string playerName =
            PlayerPrefs.GetString("FinalPlayerName", "Player");

        int level =
            PlayerPrefs.GetInt("FinalLevel", 1);

        int score =
            PlayerPrefs.GetInt("FinalScore", 0);

        float finalTime =
            PlayerPrefs.GetFloat("FinalTime", 0);

        string url =
            saveUrl +
            "?name=" + UnityWebRequest.EscapeURL(playerName) +
            "&level=" + level +
            "&score=" + score +
            "&time=" + finalTime.ToString(
                "F1",
                // Considers formating rules regardless of computers reginal settings
                CultureInfo.InvariantCulture
            );

        using (UnityWebRequest request =
               UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result !=
                UnityWebRequest.Result.Success)
            {
                Debug.LogError(
                    "Score save error: " + request.error
                );
            }
            else
            {
                Debug.Log(
                    "Score saved: " +
                    request.downloadHandler.text
                );
            }
        }
    }

    IEnumerator LoadScores()
    {
        highScoresText.text = "Loading top scores...";

        using (UnityWebRequest request =
               UnityWebRequest.Get(scoresUrl))
        {
            yield return request.SendWebRequest();

            if (request.result ==
                UnityWebRequest.Result.Success)
            {
                DisplayTopFive(
                    request.downloadHandler.text
                );
            }
            else
            {
                highScoresText.text =
                    "Could not load scores.";

                Debug.LogError(
                    "Score loading error: " + request.error
                );
            }
        }
    }

    void DisplayTopFive(string databaseText)
    {
        List<ShooterScore> scores =
            new List<ShooterScore>();

        string[] lines = databaseText.Split('\n');

        foreach (string line in lines)
        {
            string[] parts = Regex.Split(
                line.Trim(),
                @"\s+"
            );

            if (parts.Length < 4)
                continue;

            int level = 0;
            int score = 0;
            float time = 0f;

            int last = parts.Length - 1;

            bool valid =
                int.TryParse(parts[last - 2], out level) &&
                int.TryParse(parts[last - 1], out score) &&
                float.TryParse(
                    parts[last],
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out time
                );

            if (!valid)
                continue;

            string playerName = "";

            for (int i = 0; i < parts.Length - 3; i++)
            {
                if (i > 0)
                    playerName += " ";

                playerName += parts[i];
            }

            ShooterScore entry = new ShooterScore();
            entry.playerName = playerName;
            entry.level = level;
            entry.score = score;
            entry.time = time;

            scores.Add(entry);
        }

        scores.Sort(
            (a, b) => b.score.CompareTo(a.score)
        );

        string display =
            "TOP 5 SCORES\n" +
            "Name     Level  Score  Time\n";

        int numberToShow = Mathf.Min(5, scores.Count);

        for (int i = 0; i < numberToShow; i++)
        {
            ShooterScore entry = scores[i];

            display +=
                entry.playerName + "     " +
                entry.level + "     " +
                entry.score + "     " +
                entry.time.ToString("F1") + "\n";
        }

        highScoresText.text = display;
    }
}