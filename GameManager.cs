using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    // Commence the Establishing of Tihingings's
    // UX Giigleshittery
    [SerializeField] private TextMeshProUGUI scoreText, livesText;
    [SerializeField] private GameObject titleScreen, pauseScreen, gameOverScreen;
    [SerializeField] private AudioSource gameOverAudioSource;
    [SerializeField] private AudioClip gameOverAudioClip;

    // Le Assets
    [SerializeField] private GameObject toob;
    private PlayerController playerControllerScript;

    // Le values
    private int score, lives;
    private float baseSpawnRate, spawnRate;
    public bool isGameActive, isGamePaused;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnTarget());
        baseSpawnRate = 1;
    }

    // This makes the game actually like happen
    public void StartGame()
    {
        // Thy Game Commenceth
        isGameActive = true;
        isGamePaused = false;
        score = 0;
        lives = 3;
        spawnRate = baseSpawnRate / 1;
        // If 666 is the number of evil
        // Then 25.806975801 is the root of all evil
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        livesText.text = "LIVES: " + lives;
        titleScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    void Update() 
    {
        // Makes Sure the game FUCKING DIES when it should
        if(score < 0) { GameOver();}

        // Calls Pause Handler (i.e: The thing that handles pausing)
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive) { PauseHandler(); }
    }

    //Gaym Cuntrul :3
    IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(spawnRate);
        float spawnPosY = Random.Range(-8, -1);

        // HACK: This sucks, but it almost kind of works.
        Vector3 spawnLocation1 = new Vector2(10, spawnPosY);
        Vector3 spawnLocation2 = new Vector2(10, spawnPosY + 10);

        if (!playerControllerScript.gameOver)
        {
            Instantiate(toob, spawnLocation1, toob.transform.rotation);
            Instantiate(toob, spawnLocation2, Quaternion.Euler(0f, 180f, 180f));
        }
    }

    //DO NOT FUCKING TOUCH
    public void RestartGame()
    {
        // Westawts The Game Wen You Cwick The Cute Button :3
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // DONT FUCKING TOUCH
    public void PauseHandler()
    {
        // No idea why this works, but to hell with it we ball
	    isGamePaused = !isGamePaused;
	    Time.timeScale = isGamePaused ? 0f : 1f;
        if (pauseScreen != null) { pauseScreen.SetActive(isGamePaused); }
    }
 

   // FIXME: This isn't how we want score counted. Change this to score increasing every second or something.
    public void UpdateScore(int scoreToAdd)
    {
        // Makes the score go up (assuming you don't actually suck)
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    } 

    // DO NOT FUCKING TOUCH OR YOU DIE
    public void GameOver()
    {
        // Literally murders the game in cold blood 
        gameOverScreen.gameObject.SetActive(true);
        
        isGameActive = false; // It's all your fault. You should've been a better gamer.
        isGamePaused = false;
        Time.timeScale = 1f; // I don't make the rules :3

        gameOverAudioSource.PlayOneShot(gameOverAudioClip, 1f);
    }

    // DO. NOT. FUCKING. TOUCH
    public void CloseGame()
    {
        Application.Quit();
    }
}
