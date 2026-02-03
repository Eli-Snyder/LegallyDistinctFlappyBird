using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject toob;
    private float spawnDelay = 3;
    private float spawnInterval = 1.5f;

    private PlayerController playerControllerScript;


    void Start()
    {
        // HACK: Horrible no good very bad hack of hacks
        // HOWEVER: I guess it works :(
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void SpawnObjects ()
    {
        float spawnPosY = Random.Range(-8, -1);

        // HACK: This sucks, but it almost kind of works.
        Vector3 spawnLocation1 = new Vector2(10, spawnPosY);
        Vector3 spawnLocation2 = new Vector2(10, spawnPosY+10);

        if (!playerControllerScript.gameOver)
        {
            Instantiate(toob, spawnLocation1, toob.transform.rotation);
            Instantiate(toob, spawnLocation2 , Quaternion.Euler(0f, 180f, 180f));
        }

    }
}
