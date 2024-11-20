using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    public GameObject RightHandCo;

    Vector3 previousRightHandPosition = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(previousRightHandPosition, RightHandCo.transform.position);
        
        // Set a base value for the time scale
        float baseTimeScale = 1.0f; // Normal speed
        float maxDistance = 0.2f; // The distance at which time should slow down the most

        // Modify the time scale based on the distance moved by the hand
        // The more the hand moves, the lower the time scale will be
        float newTimeScale = Mathf.Clamp(baseTimeScale - distance*100 / maxDistance, 0.1f, 1.0f);

        // Debug.Log("Distance: " + newTimeScale);

        // Apply the new time scale to slow down or speed up the game
        Time.timeScale = newTimeScale;

        previousRightHandPosition = RightHandCo.transform.position;
        
    }
}