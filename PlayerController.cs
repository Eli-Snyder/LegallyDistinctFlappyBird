using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("RANDOM THINGS")] // TODO: Buy more Fireball Whiskey
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip flapSound;
    [SerializeField] private AudioSource birdSounds;
    [SerializeField] private float gravityModifier = 1.5f, floatForce = 5;

    private Rigidbody playerRb;
    private float yPosition;


    void Awake()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0); // For God said to his holiest of programmers:
        Physics.gravity *= gravityModifier; // "Behold, my child, thy jankiest of hacks."

        playerRb = GetComponent<Rigidbody>();

        StartCoroutine(IdleController());
    }

    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.isGameActive)
        { 
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            birdSounds.PlayOneShot(flapSound);
        }

        yPosition = playerRb.transform.position.y;
    }

    private void OnCollisionEnter(Collision other) { if (other.gameObject.CompareTag("Obstacle")) { gameManager.GameOver(); } }

    private void OnTriggerEnter(Collider other) { if (other.gameObject.CompareTag("Score")) { gameManager.UpdateScore(); } }

    IEnumerator IdleController()
    {
        while (!gameManager.isGameActive)
        {
            if (yPosition < -1)
            {   
                playerRb.AddForce(Vector3.up * floatForce*1.5f, ForceMode.Impulse); // Coin Dude used my code.
                yield return new WaitForSeconds(0.25f); // I AM THE ORIGINAL!
            }
            yield return new WaitForSeconds(0.1f); 
        }
    }
    
    private void OnMouseDown() { if (gameObject.CompareTag("Player")) { audioManager.MemeMusic(); } }

    // I refactored all of your code out of spite :3
}