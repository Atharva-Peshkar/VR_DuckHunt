using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CubeEnemy : MonoBehaviour
{
    public GameObject explosionVFX; 
    public float explosionDelay = 0f; 

    private void OnTriggerEnter(Collider other)
    {
        // // Check if the collided object is the player
        // if (other.CompareTag("MainCamera")) 
        // {

        //     ActivateExplosion();
            
        // }

        if (other.CompareTag("redBulletObj")) 
        {

            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            // Debug.Log("Bullet hit the target!");
            Destroy(gameObject); // Destroy the target object on bullet hit
            
        }
    }

    // private void ActivateExplosion()
    // {
    //     // Instantiate the explosion VFX at the enemy's position
    //     Instantiate(explosionVFX, transform.position, Quaternion.identity);
        
    //     // Destroy the enemy instance
    //     Destroy(gameObject);

    //     SceneManager.LoadScene("Scenes/GameOver");
    // }


    // private void DestroyExplosion()
    // {
    //     // Instantiate the explosion VFX at the enemy's position
    //     Instantiate(explosionVFX, transform.position, Quaternion.identity);
        
    //     // Destroy the enemy instance
    //     Destroy(gameObject);
    // }
    
    // private void FreezeGame()
    // {
    //     Time.timeScale = 0f; // Freeze the game
    // }
}
