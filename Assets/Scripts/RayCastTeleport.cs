using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class RayCastTeleport : MonoBehaviour
{
    [SerializeField]
    private GameObject playerOrigin;
    [SerializeField]
    private LayerMask teleportMask;
    [SerializeField]
    private InputActionReference teleportButtonPress;

    void Start()
    {
        teleportButtonPress.action.performed += DoRayCast;
    }

    void DoRayCast(InputAction.CallbackContext _)
    {
        RaycastHit hit;

        bool didHit = Physics.Raycast(
        transform.position,
        transform.forward,
        out hit,
        Mathf.Infinity,
        teleportMask);
        
    if (didHit) {
        // The object we hit will be in the collider property of our hit object.
        // We can get the name of that object by accessing it's Game Object then the name property
        Debug.Log(hit.collider.gameObject.name);
        
				// Don't forget to attach the player origin in the editor!
        playerOrigin.transform.position = hit.collider.gameObject.transform.position;
    }
        
    }
}
