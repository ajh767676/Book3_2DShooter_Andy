using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject bullet;
    public float moveSpeed = 3f;

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            movement += Vector3.left;

        if (Input.GetKey(KeyCode.RightArrow))
            movement += Vector3.right;

        if (Input.GetKey(KeyCode.UpArrow))
            movement += Vector3.up;

        if (Input.GetKey(KeyCode.DownArrow))
            movement += Vector3.down;

        transform.Translate(
            movement.normalized * moveSpeed * Time.deltaTime
        );

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Instantiate(
                bullet,
                transform.position + transform.up * 0.8f,
                Quaternion.identity
            );

            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.simulated = true;
            rb.WakeUp();
            rb.linearVelocity = transform.up * 10f;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (GameController.instance != null)
            {
                GameController.instance.EndGame(
                    "Your spaceship was destroyed!"
                );
            }
        }
    }
}