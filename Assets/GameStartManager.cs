using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public InputActionReference triggerLeft;
    public InputActionReference triggerRight;

    // Update is called once per frame
    void Update()
    {
        float leftTriggerHeld = triggerLeft.action.ReadValue<float>();
        float rightTriggerHeld = triggerRight.action.ReadValue<float>();

        if (leftTriggerHeld > 0.5 && rightTriggerHeld > 0.5) {
            SceneManager.LoadScene("Scenes/MainScene");
        }
    }
}