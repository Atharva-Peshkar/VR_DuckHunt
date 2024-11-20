using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour
{
    public GameObject cubeEnemyPrefab;  
    public Transform player;             
    public int groupSize = 3;            // Number of enemies in the group
    public float initialSpawnRadius = 20f; // Starting radius for the first spawn group
    public float spawnRadiusIncrement = 2f; // How much closer to the player each group spawns
    public float spawnInterval = 1f;      // Time interval for spawning new enemy groups

    private GameObject[] currentEnemies; // Array to keep track of current enemy instances
    private float currentSpawnRadius;     // Current spawn radius for enemies

    void Start()
    {
        currentEnemies = new GameObject[groupSize];
        currentSpawnRadius = initialSpawnRadius; 
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
                currentEnemies[i] = enemy; 
            }

            // Move new enemies towards the player
            StartCoroutine(MoveEnemiesTowardsPlayer());

            // Decrease the spawn radius for the next group
            currentSpawnRadius -= spawnRadiusIncrement;
            if (currentSpawnRadius < 5f) 
            {
                currentSpawnRadius = 5f; // Prevent spawning too close to the player
            }

            yield return new WaitForSeconds(spawnInterval); 
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
                    enemy.transform.position += direction * Time.deltaTime * 5f; 
                }
            }
            yield return null; 
        }
    }
}


