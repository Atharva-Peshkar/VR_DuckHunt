using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosionAudio : MonoBehaviour
{
    public AudioClip ExplosionSFX;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the enemy
        if (other.CompareTag("NewEnemy")) 
        {
            GetComponent<AudioSource>().PlayOneShot(ExplosionSFX);
            
        }
    }
}
