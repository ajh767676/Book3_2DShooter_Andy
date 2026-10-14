using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        int speedSetting = PlayerPrefs.GetInt("EnemySpeed", 1);

        if (speedSetting == 0)
        {
            speed = 1f;       // Slow
        }
        else if (speedSetting == 1)
        {
            speed = 2f;       // Normal
        }
        else
        {
            speed = 3.5f;     // Fast
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}