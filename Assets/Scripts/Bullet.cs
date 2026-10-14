using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(
                explosionPrefab,
                collision.transform.position,
                Quaternion.identity
            );

            GameController.instance.AddScore(10);
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}