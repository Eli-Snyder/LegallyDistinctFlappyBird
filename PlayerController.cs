using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;

    [SerializeField] private float gravityModifier = 1.5f, floatForce = 5;
    [SerializeField] private GameManager gameManager; // Assign GameManager GameObject in the editor.
    private Rigidbody playerRb;


    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    { 
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        } 
    }

}
