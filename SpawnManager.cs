using UnityEngine;
using System.Collections;
// TODO: Rewrite spawn manager to just generally suck less.

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject toob;
    [SerializeField] private float baseSpawnRate;
    [SerializeField] private GameManager gameManager;

    private Coroutine spawnRoutine;
    private float spawnRate;

    

    void Start()
    {
        spawnRate = baseSpawnRate;
        StartCoroutine(SpawnTarget()); // TODO: Will add activation trigger later
    }

    public void StartSpawner() // Called by GameManager to start the spawner.
    {
        if (SpawnTarget == null)
        { spawnRoutine = StartCoroutine(SpawnTarget()); }
    }

    IEnumerator SpawnTarget()
    {
        while (gameManager.isGameActive)
        {
            spawnRate += 0.5f;
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
}
