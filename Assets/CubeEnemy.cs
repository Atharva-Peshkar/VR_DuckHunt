// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;


// public class CubeEnemy : MonoBehaviour
// {
//     public GameObject explosionVFX; 
//     public float explosionDelay = 0f; 

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("redBulletObj")) 
//         {

//             Instantiate(explosionVFX, transform.position, Quaternion.identity);
//             // Debug.Log("Bullet hit the target!");
//             Destroy(gameObject); // Destroy the target object on bullet hit
            
//         }
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuckCollision : MonoBehaviour
{
    public System.Action OnDuckHit; // Delegate for score update
    public System.Action OnDuckDestroyed; // Delegate to track destruction
    public GameObject explosionVFX; // Optional explosion effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("redBulletObj")) // Check if a bullet hits the duck
        {
            if (explosionVFX != null)
            {
                Instantiate(explosionVFX, transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject); // Destroy the bullet
            Destroy(gameObject); // Destroy the duck

            OnDuckHit?.Invoke(); // Update the score
            OnDuckDestroyed?.Invoke(); // Notify spawner of duck destruction
        }
    }
}
