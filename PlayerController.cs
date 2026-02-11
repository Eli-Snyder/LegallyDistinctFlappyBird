using System.Collections;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header ("RANDOM THINGS")]
    [SerializeField] private float gravityModifier = 1.5f, floatForce = 5;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip flapSound, memeSound;
    [SerializeField] private AudioSource birdSounds, memeryAudio;
    
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

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with the opps, set gameOver to true
        if (other.gameObject.CompareTag("Obstacle"))
        { gameManager.GameOver(); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        { gameManager.UpdateScore(); } // I don't answer to corpbro soyjak chuds :3
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Player"))
        {
            memeryAudio.PlayOneShot(memeSound);
        }
    }

    IEnumerator IdleController()
    {
        while (!gameManager.isGameActive)
        {
            if (yPosition < -1)
            {   // Coin Dude used my code.
                // I AM THE ORIGINAL!
                playerRb.AddForce(Vector3.up * floatForce*1.5f, ForceMode.Impulse);
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.1f); 
        }
    }
}
