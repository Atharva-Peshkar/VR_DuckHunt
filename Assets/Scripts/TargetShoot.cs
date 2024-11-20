using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShoot : MonoBehaviour
{
    public GameObject BlueTarget;
    GameObject blueTargetInstance;
    
    // Start is called before the first frame update
    void Start()
    {
         // To keep track of instantiated target
        // blueTargetInstance = Instantiate(BlueTarget, transform.position, transform.rotation);
        // blueTargetInstance.GetComponent<Rigidbody>().AddForce(transform.forward);

    }

    // Update is called once per frame
    // void Update()
    // {
    //     // Movement settings
    //     float movementSpeed = 2f; // Speed of the movement
    //     float movementRange = 3f; // Range of movement back and forth

    //     // Calculate new X position
    //     float newX = Mathf.Sin(Time.time * movementSpeed) * movementRange;
        
    //     // Update object's position
    //     transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    // }

    void OnCollisionEnter(Collision collision)
    {
        // Log the tag of the colliding object
        // Debug.Log("Collided with object tagged: " + collision.gameObject.tag);

        // Ensure the target detects a bullet collision by checking for the correct tag
        if (collision.gameObject.CompareTag("redBulletObj"))
        {
            // Debug.Log("Bullet hit the target!");
            Destroy(gameObject); // Destroy the target object on bullet hit
        }
    }
}
