using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuckCollision : MonoBehaviour
{
    public System.Action OnDuckHit; 
    public System.Action OnDuckDestroyed; 
    public GameObject explosionVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("redBulletObj")) // Check if a bullet hits the duck
        {
            if (explosionVFX != null)
            {
                Instantiate(explosionVFX, transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject); 
            Destroy(gameObject); // Destroy the duck

            OnDuckHit?.Invoke(); // Update the score
            OnDuckDestroyed?.Invoke(); // Notify spawner of duck destruction
        }
    }
}
