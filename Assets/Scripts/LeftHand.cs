using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public GameObject LeftHandCo;
    Vector3 previousLeftHandPosition = new Vector3(0,0,0);
    // public Transform other;
    // public float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(previousLeftHandPosition, LeftHandCo.transform.position);

        // Set a base value for the time scale
        float baseTimeScale = 1.0f; // Normal speed
        float maxDistance = 0.2f; // The distance at which time should slow down the most

        // Modify the time scale based on the distance moved by the hand
        // The more the hand moves, the lower the time scale will be
        float newTimeScale = Mathf.Clamp(baseTimeScale - distance*100 / maxDistance, 0.1f, 1.0f);

        // Apply the new time scale to slow down or speed up the game
        Time.timeScale = newTimeScale;

        previousLeftHandPosition = LeftHandCo.transform.position;
        
    }
}
