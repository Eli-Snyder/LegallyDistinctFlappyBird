using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.Audio;

// TODO: Spawn system is sad. Pls fix.
// TODO: Make a UI so things actually happen.

public class GameManager : MonoBehaviour
{
    // Commence the Establishing of Tihingings's
    // UX Silliness
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject titleScreen, pauseScreen, gameOverScreen;

    // Le Things
    [SerializeField] private GameObject toob;
    private PlayerController playerControllerScript;

    // Le Values
	[SerializeField] private float baseSpawnRate;
    private int score;
    private float spawnRate;
    public bool isGameActive, isGamePaused;

	
    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        StartGame(1f)
    }

    // This makes the game actually like happen
    public void StartGame(float difficulty)
    {
        // Thy Game Commenceth
        isGameActive = true;
        isGamePaused = false;
        score = 0;
        spawnRate = baseSpawnRate / difficulty;

        StartCoroutine(SpawnTarget());
        
		UpdateScore(0);

        titleScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    void Update() 
    {
        // Calls Pause Handler (i.e: The thing that handles pausing)
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive) { PauseHandler(); }
    }

    //Gaym Cuntrul :3
    IEnumerator SpawnTarget()
    {
        
        if (!playerControllerScript.gameOver)
        {
			yield return new WaitForSeconds(spawnRate);
        	float spawnPosY = Random.Range(-8, -1);
			
			// No, I don't know why Vector3 doesn't freak out when it's set to equal a Vector2.
			// All I know is that it works, and therefore will not be touched until I feel like it.
			Vector3 spawnLocation1 = new Vector2(10, spawnPosY);
        	Vector3 spawnLocation2 = new Vector2(10, spawnPosY + 10);

            Instantiate(toob, spawnLocation1, toob.transform.rotation);
            Instantiate(toob, spawnLocation2, Quaternion.Euler(0f, 180f, 180f));
        }
    }

    // Do not touch yet
    public void RestartGame()
    {
        // TODO: Button doesn't exist yet.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	
    // Leave it for now
    public void PauseHandler()
    {
        // Hopefully this works like it did last time.
	    isGamePaused = !isGamePaused;
	    Time.timeScale = isGamePaused ? 0f : 1f;
        if (pauseScreen != null) { pauseScreen.SetActive(isGamePaused); }
    }
 

   // FIXME: This isn't how we want score counted. Change this to score increasing every second or something.
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    } 

    // Leave it
    public void GameOver()
    {
        // Literally murders the game in cold blood 
        gameOverScreen.gameObject.SetActive(true);
        
        isGameActive = false; // It's all your fault. You should've been a better gamer.
        isGamePaused = false;
        Time.timeScale = 1f; // I don't make the rules :3

        gameOverAudioSource.PlayOneShot(gameOverAudioClip, 1f);
    }

    // Exactly what it says on the tin.
    public void CloseGame() {Application.Quit();} // DO. NOT. TOUCH.
}

