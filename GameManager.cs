using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Commence the Establishing of Tihingings's
    [Header("UI Silliness")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject titleScreen, persistentUI, pauseScreen;
    [SerializeField] private GameObject gameOverScreen1, gameOverScreen2;
    [SerializeField] private GameObject restartButton, exitButton;

    [Header("Le Objects")]
    [SerializeField] private SpawnManager spawnManager; 
    [SerializeField] private AudioClip gameOverSound;   
    [SerializeField] private AudioSource playerAudio;
    public static GameManager Instance;

    [Header("Le Values")]
    [SerializeField] private int score; // God give me amphetamines to change the things I can change
    public bool isGameActive, isGamePaused; // And Fireball Whiskey to pretend that the things I can't change just aren't real


    private void Awake()
    {
        isGameActive = false;
        Time.timeScale = 1f;

        // Creates an instance of GameManager if one does not already exist.
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        if (titleScreen != null) { titleScreen.SetActive(true); }
        if (pauseScreen != null) { pauseScreen.SetActive(false); }
        if (persistentUI != null) { persistentUI.SetActive(false); }
        if (restartButton != null) { restartButton.SetActive(false); }
        if (exitButton != null) { exitButton.SetActive(true); }

        if (gameOverScreen1 != null) { gameOverScreen1.SetActive(false); }
        if (gameOverScreen2 != null) { gameOverScreen2.SetActive(false); }
    }

    // This makes the game actually like happen
    public void StartGame()
    {
        // Thy Game Commenceth
        isGameActive = true;
        isGamePaused = false;
        score = 0;

        if (titleScreen != null) { titleScreen.SetActive(false); } // Born To Die
        if (pauseScreen != null) { pauseScreen.SetActive(false); } // World is a fuck
        if (persistentUI != null) { persistentUI.SetActive(true); } // Kill em all 1989
        if (restartButton != null) { restartButton.SetActive(false); } // I am trash man
        if (exitButton != null) { exitButton.SetActive(false); } // 410,757,864,530 EMPTY TRASH CANS

        if (gameOverScreen1 != null) { gameOverScreen1.SetActive(false); }
        if (gameOverScreen2 != null) { gameOverScreen2.SetActive(false); }


        spawnManager.StartSpawner();
    }

    void Update() { if (Input.GetKeyDown(KeyCode.Escape) && isGameActive) { PauseHandler(); } } // I call this code refactor "spite"

    public void RestartGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); } // This is also spite.
	
    public void PauseHandler()
    {
	    isGamePaused = !isGamePaused;
	    Time.timeScale = isGamePaused ? 0f : 1f;

        if (pauseScreen != null) { pauseScreen.SetActive(isGamePaused); } // How the 9mm round in the chamber of my Sig P320
        if (restartButton != null) { restartButton.SetActive(isGamePaused); } // looks at my dick and balls when I holster it
        if (exitButton != null) { exitButton.SetActive(isGamePaused); } // at the 1 o'clock position
        if (persistentUI != null) { persistentUI.SetActive(!isGamePaused); } // :3
    }

    public void UpdateScore()
    {
        if (isGameActive)
        {
            score++;
            scoreText.text = "SCORE: " + score;
        }
    }
    
    public void GameOver()
    {
        if (gameOverScreen2 != null && score>10) 
        { 
            gameOverScreen2.gameObject.SetActive(true); // Wow you're really good at this!
            gameOverScreen1.gameObject.SetActive(false);
        }
        else
        {
            gameOverScreen1.gameObject.SetActive(true); // Holy shit you genuinely suck at this game lmao
            gameOverScreen2.gameObject.SetActive(false);
        }
        if (restartButton != null) { restartButton.SetActive(true); }
        if (exitButton != null) { exitButton.SetActive(true); }
        
        isGameActive = false; 
        isGamePaused = false;
        Time.timeScale = 1f;

        playerAudio.PlayOneShot(gameOverSound); // I hope you like the sound of metalpipe.wav
	}
	
    public void CloseGame() { Application.Quit(); } // ALL MY HOMIES HATE WHITESPACE RAAAAAAAAH
}