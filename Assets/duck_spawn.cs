using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duckPrefab; 
    public int duckCount = 10; // Number of ducks to spawn
    public float duckSpeed = 3.0f; // Speed of the ducks
    public float spawnDelay = 0.5f; // Delay between spawning each duck

    private int score = 0; // Keeps track of the score
    private int ducksDestroyed = 0; // Tracks how many ducks have been destroyed
    private List<GameObject> ducks = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnDucksSequentially());
    }

    // Increments score when a duck is hit
    public void AddScore()
    {
        score++;
    }

    // Called when a duck is destroyed to track remaining ducks
    public void OnDuckDestroyed()
    {
        ducksDestroyed++;

        if (ducksDestroyed >= duckCount)
        {
            EndGame(); // Transition to the end screen when all ducks are gone
        }
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("FinalScore", score); // Save the score for the next scene
        SceneManager.LoadScene("Scenes/GameOver");
    }

    IEnumerator SpawnDucksSequentially()
    {
        for (int i = 0; i < duckCount; i++)
        {

            if (i >= duckCount-1)
            {
                EndGame(); // Transition to the end screen when all ducks are gone
            }

            Vector3 spawnPosition = new Vector3(
                23f, // Fixed X value
                0f,  // Start at Y = 0
                Random.Range(11f, 36f) // Random Z value between 11 and 36
            );

            GameObject duck = Instantiate(duckPrefab, spawnPosition, Quaternion.identity);
            ducks.Add(duck);

            DuckMovement duckMovement = duck.AddComponent<DuckMovement>();
            duckMovement.speed = duckSpeed;
            duckMovement.yEnd = 50f;
            duckMovement.zBounds = new Vector2(11f, 36f);

            DuckCollision duckCollision = duck.AddComponent<DuckCollision>();
            duckCollision.OnDuckHit = AddScore; 
            duckCollision.OnDuckDestroyed = OnDuckDestroyed; 

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}


public class DuckMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float yEnd = 50f;  // Target Y position (where ducks fly up to)
    public Vector2 zBounds;   // Z movement bounds

    private Vector3 direction;
    private float directionChangeInterval = 0.5f; // Change direction every 0.5 seconds

    void Start()
    {
        // Initial random movement direction in Z (duck only moves in the XY plane)
        direction = new Vector3(
            0f, 
            1f,
            Random.Range(-2f, 2f)
        ).normalized;

        // Start changing direction on the Z-axis at regular intervals
        StartCoroutine(ChangeZDirection());
    }

    void Update()
    {
        // Move vertically upwards
        transform.position += direction * speed * Time.deltaTime;

        // Move randomly along the Z axis, bouncing within bounds
        if (transform.position.z > zBounds.y || transform.position.z < zBounds.x)
        {
            direction.z = -direction.z;
        }

        // Check if the duck has reached the target vertical position (y = 50)
        if (transform.position.y >= yEnd)
        {
            direction.y = 0f; // Stop moving upwards after reaching y = 50
        }
    }

    // Coroutine to randomly change the direction along the Z-axis every 0.5 seconds
    IEnumerator ChangeZDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(directionChangeInterval);
            direction.z = Random.Range(-2f, 2f); // Increased magnitude of direction change
        }
    }
}


