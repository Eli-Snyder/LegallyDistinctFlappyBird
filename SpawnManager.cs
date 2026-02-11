using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header ("Much Wow")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject toob, scoreUpdater;
    [SerializeField] private float minSpawnRate = 1f, maxSpawnRate = 4f, difficultyRamp = 0.01f;

    [Header ("Monitoring")]
    [SerializeField] private float difficulty;
    private Coroutine spawnRoutine;


    void Start()
    { difficulty = 0f; }

    void Update()
    {
        if (!gameManager.isGameActive) return;

        difficulty += difficultyRamp * Time.deltaTime;
        difficulty = Mathf.Clamp01(difficulty);
    }


    public void StartSpawner() // Called by GameManager to start the spawner.
    {
        if (spawnRoutine == null)
        { spawnRoutine = StartCoroutine(SpawnTarget()); }
    }


    IEnumerator SpawnTarget()
    {
        while (gameManager.isGameActive)
        {
            float interval = Mathf.Lerp(maxSpawnRate, minSpawnRate, difficulty);
            yield return new WaitForSeconds(interval);

            float spawnPosY = Random.Range(-8, -1);
            Vector3 spawnLocation1 = new Vector2(10, spawnPosY);
            Vector3 spawnLocation2 = new Vector2(10, spawnPosY + 10); // No, I don't know why Vector3 doesn't freak out when it's set to equal a Vector2.
            Vector3 spawnLocation3 = new Vector2(10, spawnPosY + 5); // All I know is that it works, and therefore will not be touched until I feel like it.

            Instantiate(toob, spawnLocation1, toob.transform.rotation);
            Instantiate(toob, spawnLocation2, Quaternion.Euler(0f, 180f, 180f));
            Instantiate(scoreUpdater, spawnLocation3, scoreUpdater.transform.rotation);
        }
    }
}