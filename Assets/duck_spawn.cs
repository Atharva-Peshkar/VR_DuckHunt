// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class duck_spawn : MonoBehaviour
// {
//     public GameObject duckPrefab; // Prefab for the duck
//     public int duckCount = 10; // Number of ducks to spawn
//     public Vector3 spawnArea = new Vector3(10, 5, 10); // Area where ducks can spawn
//     public float duckSpeed = 3.0f; // Speed of the ducks
//     public Vector3 movementBounds = new Vector3(15, 10, 0); // Area where ducks can move

//     private List<GameObject> ducks = new List<GameObject>();

//     void Start()
//     {
//         SpawnDucks();
//     }

//     void SpawnDucks()
//     {
//         for (int i = 0; i < duckCount; i++)
//         {
//             Vector3 randomPosition = new Vector3(
//                 Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
//                 Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
//                 Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
//             );

//             GameObject duck = Instantiate(duckPrefab, randomPosition, Quaternion.identity);
//             ducks.Add(duck);

//             // Attach a movement script to the duck
//             DuckMovement duckMovement = duck.AddComponent<DuckMovement>();
//             duckMovement.speed = duckSpeed;
//             duckMovement.bounds = movementBounds;
//         }
//     }
// }

// public class DuckMovement : MonoBehaviour
// {
//     public float speed = 3.0f;
//     public Vector3 bounds;

//     private Vector3 direction;

//     void Start()
//     {
//         // Assign a random initial direction
//         direction = new Vector3(
//             Random.Range(-1f, 1f),
//             Random.Range(-1f, 1f),
//             0 // Ducks move in the XY plane
//         ).normalized;
//     }

//     void Update()
//     {
//         transform.position += direction * speed * Time.deltaTime;

//         // Check bounds and bounce
//         if (transform.position.x > bounds.x || transform.position.x < -bounds.x)
//         {
//             direction.x = -direction.x;
//         }

//         if (transform.position.y > bounds.y || transform.position.y < -bounds.y)
//         {
//             direction.y = -direction.y;
//         }
//     }
// }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class duck_spawn : MonoBehaviour
// {
//     public GameObject duckPrefab; // Prefab for the duck
//     public int duckCount = 10; // Number of ducks to spawn
//     public Vector3 spawnArea = new Vector3(10, 10, 0); // Area where ducks can spawn
//     public float duckSpeed = 3.0f; // Speed of the ducks
//     public Vector3 movementBounds = new Vector3(15, 10, 0); // Area where ducks can move
//     public float spawnDelay = 0.5f; // Delay between spawning each duck

//     private List<GameObject> ducks = new List<GameObject>();

//     void Start()
//     {
//         StartCoroutine(SpawnDucksSequentially());
//     }

//     IEnumerator SpawnDucksSequentially()
//     {
//         for (int i = 0; i < duckCount; i++)
//         {
//             Vector3 randomPosition = new Vector3(
//                 Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
//                 Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
//                 Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
//             );

//             GameObject duck = Instantiate(duckPrefab, randomPosition, Quaternion.identity);
//             ducks.Add(duck);

//             // Attach a movement script to the duck
//             DuckMovement duckMovement = duck.AddComponent<DuckMovement>();
//             duckMovement.speed = duckSpeed;
//             duckMovement.bounds = movementBounds;

//             yield return new WaitForSeconds(spawnDelay); // Wait before spawning the next duck
//         }
//     }
// }

// public class DuckMovement : MonoBehaviour
// {
//     public float speed = 3.0f;
//     public Vector3 bounds;

//     private Vector3 direction;

//     void Start()
//     {
//         // Assign a random initial direction
//         direction = new Vector3(
//             Random.Range(-1f, 1f),
//             Random.Range(-1f, 1f),
//             0 // Ducks move in the XY plane
//         ).normalized;
//     }

//     void Update()
//     {
//         transform.position += direction * speed * Time.deltaTime;

//         // Check bounds and bounce
//         if (transform.position.x > bounds.x || transform.position.x < -bounds.x)
//         {
//             direction.x = -direction.x;
//         }

//         if (transform.position.y > bounds.y || transform.position.y < -bounds.y)
//         {
//             direction.y = -direction.y;
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duckPrefab; // Prefab for the duck
    public int duckCount = 10; // Number of ducks to spawn
    public float duckSpeed = 3.0f; // Speed of the ducks
    public float spawnDelay = 0.5f; // Delay between spawning each duck

    private List<GameObject> ducks = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnDucksSequentially());
    }

    IEnumerator SpawnDucksSequentially()
    {
        for (int i = 0; i < duckCount; i++)
        {
            // Spawn position constrained to x: 23, y: 0, z: 11–36
            Vector3 spawnPosition = new Vector3(
                23f, // Fixed X value
                0f,  // Start at Y = 0
                Random.Range(11f, 36f) // Random Z value between 11 and 36
            );

            GameObject duck = Instantiate(duckPrefab, spawnPosition, Quaternion.identity);
            ducks.Add(duck);

            // Attach a movement script to the duck
            DuckMovement duckMovement = duck.AddComponent<DuckMovement>();
            duckMovement.speed = duckSpeed;
            duckMovement.yEnd = 50f; // Ducks will move until y = 50
            duckMovement.zBounds = new Vector2(11f, 36f); // Movement bounds in Z direction

            yield return new WaitForSeconds(spawnDelay); // Wait before spawning the next duck
        }
    }
}

public class DuckMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float yEnd = 50f;  // Target Y position (where ducks fly up to)
    public Vector2 zBounds;   // Z movement bounds (min, max)

    private Vector3 direction;
    private float directionChangeInterval = 0.5f; // Change direction every 0.5 seconds

    void Start()
    {
        // Initial random movement direction in Z (duck only moves in the XY plane)
        direction = new Vector3(
            0f, // Ducks move only along Y-axis (vertical)
            1f, // Move upwards
            Random.Range(-2f, 2f) // Increased Z direction range (-2 to 2)
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
            direction.z = -direction.z; // Reverse Z direction when out of bounds
        }

        // Check if the duck has reached the target vertical position (y = 50) and stop further movement upwards
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

