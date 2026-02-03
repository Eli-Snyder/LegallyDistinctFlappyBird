using UnityEngine;

// TODO: Rewrite spawn manager to just generally suck less.

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject toob; 
    [SerializeField] private float baseSpawnRate;
    private float spawnRate;

    private PlayerController playerControllerScript;


    void Start()
    {
        spawnRate = baseSpawnRate;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        StartCoroutine(SpawnTarget()); // TODO: Will add activation trigger later
    }


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
}
