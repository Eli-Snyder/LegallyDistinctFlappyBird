using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

// TODO: Make a UI so things actually happen.

public class GameManager : MonoBehaviour
{
    // Commence the Establishing of Tihingings's
    // UI Silliness
	[SerializeField] private GameObject titleScreen, pauseScreen, gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Le Things
	public static GameManger Instance;
    [SerializeField] private SpawnManager spawnManager;
	
    // Le Values
	public bool isGameActive, isGamePaused;
	private int score;


	private void Awake()
	{
		// Creates an instance of GameManager if one does not already exist.
		if (Instance == null)
		{ Instance = this; }
		else
		{ Destroy(gameObject); }
	}
    
	// This makes the game actually like happen
    public void StartGame(float difficulty)
    {
        // Thy Game Commenceth
        isGameActive = true;
        isGamePaused = false;
        score = 0;

		
		UpdateScore(0);

        //if (titleScreen != null) { titleScreen.SetActive(false); }
        //if (pauseScreen != null) { pauseScreen.SetActive(false); }
		// spawnManager.SpawnTarget();
    }
	
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive) 
		{ PauseHandler(); }
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
	    isGamePaused = !isGamePaused;
	    Time.timeScale = isGamePaused ? 0f : 1f;
		
        // if (pauseScreen != null) { pauseScreen.SetActive(isGamePaused); }
    }
 

   // FIXME: This isn't how we want score counted. Probably needs to be entirely rewritten.
    public void UpdateScore(int scoreToAdd)
    {
		// Code Commented out for now
		//
        //score += scoreToAdd;
        //scoreText.text = "SCORE: " + score;
    } 

    // Leave it
    public void GameOver()
    {
        //if (gameOverScreen != null) { gameOverScreen.gameObject.SetActive(true); }
        
        isGameActive = false;
        isGamePaused = false;
        Time.timeScale = 1f;
	}
	
	// DO. NOT. TOUCH. THIS.
    public void CloseGame()
	{ Application.Quit(); }
}



