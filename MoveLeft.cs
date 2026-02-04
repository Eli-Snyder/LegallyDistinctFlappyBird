using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    private float leftBound = -10f;

    void Update()
    {
        // If game is not over, move to the left
        if (GameManager.Instance != null && GameManager.Instance.isGameActive)
        { transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World); }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        { Destroy(gameObject); }
    }
}
