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
	[SerializeField] private GameObject titleScreen, pauseScreen, gameOverScreen, persistentUI;
    [SerializeField] private GameObject restartButton, exitButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    

    // Le Things
	public static GameManager Instance;
    [SerializeField] private SpawnManager spawnManager;
	
    // Le Values
	public bool isGameActive, isGamePaused;
	private int score;


	private void Awake()
	{
		// Creates an instance of GameManager if one does not already exist.
		if (Instance == null) { Instance = this; }
		else { Destroy(gameObject); }

        if (titleScreen != null) { titleScreen.SetActive(true); }
        if (pauseScreen != null) { pauseScreen.SetActive(false); }
        if (persistentUI != null) {  persistentUI.SetActive(false);}

        if (restartButton != null) { restartButton.SetActive(false); }
        if (exitButton != null) { exitButton.SetActive(false); }

        StartGame();
	}
    
	// This makes the game actually like happen
    public void StartGame()
    {
        // Thy Game Commenceth
        isGameActive = true;
        isGamePaused = false;
        score = 0;

		
		StartCoroutine(ScoreCounter());

        if (titleScreen != null) { titleScreen.SetActive(false); }
        if (pauseScreen != null) { pauseScreen.SetActive(false); }
        if (scoreText != null) { persistentUI.SetActive(true); }

        if (restartButton != null) { restartButton.SetActive(false); }
        if (exitButton != null) { exitButton.SetActive(true); }

        spawnManager.StartSpawner();
    }
	
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive) 
		{ PauseHandler(); } // "World is a fuck" - Cohyn
    }

    // Do not touch yet
    public void RestartGame()
    { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
	
    // Leave it for now
    public void PauseHandler()
    {
	    isGamePaused = !isGamePaused;
	    Time.timeScale = isGamePaused ? 0f : 1f;
		
        if (pauseScreen != null) { pauseScreen.SetActive(isGamePaused); }
    }


    // I'm sure this is fine and will in no way bite me in the ass whatsoever.
    IEnumerator ScoreCounter()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            if (isGameActive) 
            {
                scoreText.text = "SCORE: " + score;
                score++;
            }    
        }
    }

    // Leave it
    public void GameOver()
    {
        if (gameOverScreen != null) { gameOverScreen.gameObject.SetActive(true); }
        
        isGameActive = false;
        isGamePaused = false;
        Time.timeScale = 1f;
	}
	
    public void CloseGame()
	{ Application.Quit(); }
}



