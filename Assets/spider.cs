using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spider : MonoBehaviour
{
    public GameObject cubeEnemyPrefab;  // Prefab of the cube enemy to spawn
    public Transform player;             // Reference to the player
    public int groupSize = 3;            // Number of enemies in the group
    public float initialSpawnRadius = 20f; // Starting radius for the first spawn group
    public float spawnRadiusIncrement = 0.1f; // How much closer to the player each group spawns
    public float spawnInterval = 10f;      // Time interval for spawning new enemy groups

    private GameObject[] currentEnemies; // Array to keep track of current enemy instances
    private float currentSpawnRadius;     // Current spawn radius for enemies

    void Start()
    {
        currentEnemies = new GameObject[groupSize];
        currentSpawnRadius = initialSpawnRadius; // Set the initial spawn radius
        StartCoroutine(SpawnEnemyGroup());
    }

    IEnumerator SpawnEnemyGroup()
    {
        while (true)
        {
            // Delete old enemies if they exist
            if (currentEnemies != null)
            {
                foreach (GameObject enemy in currentEnemies)
                {
                    if (enemy != null)
                    {
                        Destroy(enemy);
                    }
                }
            }

            // Spawn new enemies
            currentEnemies = new GameObject[groupSize];
            for (int i = 0; i < groupSize; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(cubeEnemyPrefab, spawnPosition, Quaternion.identity);
                
                // Make the enemy face the player
                enemy.transform.LookAt(player);
                currentEnemies[i] = enemy; // Store the reference
            }

            // Move new enemies towards the player
            StartCoroutine(MoveEnemiesTowardsPlayer());

            // Decrease the spawn radius for the next group
            currentSpawnRadius -= spawnRadiusIncrement;
            if (currentSpawnRadius < 5f) // Optional: Set a minimum spawn radius
            {
                currentSpawnRadius = 5f; // Prevent spawning too close to the player
            }

            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * currentSpawnRadius;
        randomDirection.y = 0; // Keep it on the same height level
        return player.position + randomDirection;
    }

    IEnumerator MoveEnemiesTowardsPlayer()
    {
        while (true)
        {
            foreach (GameObject enemy in currentEnemies)
            {
                if (enemy != null)
                {
                    // Move enemy towards the player
                    Vector3 direction = (player.position - enemy.transform.position).normalized;
                    enemy.transform.position += direction * Time.deltaTime * 0.5f; // Adjust speed as needed
                }
            }
            yield return null; // Wait for the next frame
        }
    }
}
