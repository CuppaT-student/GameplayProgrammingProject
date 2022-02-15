using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    // create a local reference f type Rigidbody
    private Rigidbody capsuleRB;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        // get the component - of this object instance - of the type rigidbody and assign it to our local ref
        capsuleRB = GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.ThirdPersonPlayer.Enable();
        playerInputActions.ThirdPersonPlayer.Jump.performed += Jump;
        playerInputActions.ThirdPersonPlayer.Movement.performed += Movement_Performed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.ThirdPersonPlayer.Movement.ReadValue<Vector2>();
        float speed = 2F; 
        capsuleRB.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        float speed = 5F;
        capsuleRB.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        { 
            Debug.Log("Jump!" + context.phase);
            capsuleRB.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
}