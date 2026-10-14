using UnityEngine;

public class SpawnMovingTargets : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float minX = -7f;
    public float maxX = 7f;

    private float timer;

    void Start()
    {
        int enemiesPerMinute =
            PlayerPrefs.GetInt("EnemiesPerMinute", 8);

        spawnInterval = 60f / enemiesPerMinute;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            float randomX = Random.Range(minX, maxX);

            Vector3 spawnPosition = new Vector3(
                randomX,
                transform.position.y,
                0f
            );

            Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );

            timer = 0f;
        }
    }
}