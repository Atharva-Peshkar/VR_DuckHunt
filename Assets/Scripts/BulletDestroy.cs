using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Log the tag of the colliding object
        // Debug.Log("Collided with object tagged: " + collision.gameObject.tag);

        // Ensure the target detects a bullet collision by checking for the correct tag
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            // Debug.Log("Bullet hit the target!");
            Destroy(gameObject); // Destroy the target object on bullet hit
        }
    }
}
