using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    public InputActionReference triggerLeft;

    // Update is called once per frame
    void Update()
    {
        float leftTriggerHeld = triggerLeft.action.ReadValue<float>();

        if (leftTriggerHeld > 0.5) {
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
}