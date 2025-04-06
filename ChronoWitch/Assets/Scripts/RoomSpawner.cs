using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnersRoom1;
    [SerializeField] private GameObject spawnersRoom2;
    [SerializeField] private GameObject spawnersRoom3;
    [SerializeField] private GameObject spawnersRoom4;
    [SerializeField] private RespawnTimer timer;

    public float elTime;
    private float nextRespawnTime = 40f; // Start at 13 seconds

    public void ResetSpawners(GameObject spawnerRoom)
    {
        RandomSpawner[] allSpawners = spawnerRoom.GetComponentsInChildren<RandomSpawner>();
        foreach (RandomSpawner spawner in allSpawners)
        {
            spawner.SpawnObjectAtRandom();
        }
    }

    public void Respawn()
    {
        ResetSpawners(spawnersRoom1);
        ResetSpawners(spawnersRoom2);
        ResetSpawners(spawnersRoom3);
        ResetSpawners(spawnersRoom4);
    }

    public void Update()
    {
        elTime = timer.GetElapsedTime();

        if (elTime >= nextRespawnTime)
        {
            Respawn();
            nextRespawnTime += 40f; // Schedule the next respawn
        }
    }
}
