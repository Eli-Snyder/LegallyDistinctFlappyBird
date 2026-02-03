using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField private GameManager gameManager;
    private float leftBound = -10;

    void FixedUpdate()
    {
        // If game is not over, move to the left
        if (gameManager.isGameActive)
        { transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World); }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        { Destroy(gameObject); }
    }
}
