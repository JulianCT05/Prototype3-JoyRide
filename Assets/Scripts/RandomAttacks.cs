using UnityEngine;

public class RandomAttacks : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePreFabs;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 1f;

    private float timeUntilObstacleSpawn;

    void Update()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        if (obstaclePreFabs.Length == 0) return;

        GameObject obstacleToSpawn = obstaclePreFabs[Random.Range(0, obstaclePreFabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        if (obstacleRB != null)
        {
            obstacleRB.linearVelocity = Vector2.left * obstacleSpeed;
        }

    }
}
