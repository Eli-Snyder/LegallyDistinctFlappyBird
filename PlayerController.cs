using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float gravityModifier = 1.5f, floatForce = 5;
    [SerializeField] private GameManager gameManager;
    private Rigidbody playerRb;


    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.isGameActive)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            Debug.Log("Boing");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with the opps, set gameOver to true
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
            Debug.Log("Rip broski");
        } 
    }

}
