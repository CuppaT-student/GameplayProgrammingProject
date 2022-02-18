using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    // create a fields for Rigidbody
    private Rigidbody capsuleRB;
    
    // create fields for input 
    private PlayerInputActions playerInputActions;
    private InputAction moveAction;

    // create movement fields
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 10;
    private Vector3 forceDirection = Vector3.zero;
    [SerializeField] Camera playerCamera;


    
    private void Awake()
    {
        // get the component - of this object instance - of the type rigidbody and assign it to our local ref
        capsuleRB = this.GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();
        /* old tutorial code
        playerInputActions.ThirdPersonPlayer.Enable();
        playerInputActions.ThirdPersonPlayer.Jump.performed += Jump;
        playerInputActions.ThirdPersonPlayer.Movement.performed += Movement_Performed;
        */
    }

    private void OnEnable()
    {
        playerInputActions.ThirdPersonPlayer.Jump.started += DoJump;
        moveAction = playerInputActions.ThirdPersonPlayer.Movement;
        playerInputActions.ThirdPersonPlayer.Enable();
    }
    private void onDisable()
    {
        playerInputActions.ThirdPersonPlayer.Jump.started -= DoJump;
        playerInputActions.ThirdPersonPlayer.Disable();
    }

    private void FixedUpdate()
    {
        
        forceDirection += moveAction.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * moveForce;
        forceDirection += moveAction.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * moveForce;

        capsuleRB.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        // create gravity accerlation 
        if (capsuleRB.velocity.y < 0f)
            capsuleRB.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        // create a cap for the players speed but on the horizontal plane (so gravity still increases)
        Vector3 horizontalVelocity = capsuleRB.velocity;
        horizontalVelocity.y = 0;
        // check if we have exceeded our maxspeed variable
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            //if true, we set the horizontal speed to be the max and keep the current velocity in the y axis
            capsuleRB.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * capsuleRB.velocity.y;

        // Look at function controls the direction of our rb character
        LookAt();

        /* OLD TUTORIAL CODE
        Vector2 inputVector = playerInputActions.ThirdPersonPlayer.Movement.ReadValue<Vector2>();
        float speed = 2F;
        capsuleRB.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
        */
    }


   
    private void LookAt()
    {
        Vector3 direction = capsuleRB.velocity;
        direction.y = 0f;

        // Check if we the player is giving us input & we are moving
        if (moveAction.ReadValue<Vector2>().sqrMagnitude > 0.1F && direction.sqrMagnitude > 0.1F)
            // if true then change the direction the character is looking
            this.capsuleRB.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            capsuleRB.angularVelocity = Vector3.zero;
    }

    // GetCamera Functions
    // The reason we use these functions instead of just
    // using transform.forward or transform.right
    // is because the camera likely not placed exactly in the horizontal plane
    // which we want to move our character on
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log("doJump!");
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        // define a new ray at with -
        // origin = slightly above the characters feet to ensure we cast above whatever surface the player is on
        // direction = down (4Head)
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25F, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
        {
            Debug.Log("IS GROUNDED!");
            return true;
        }
        else
        {
            Debug.Log("NOT GROUNDED!");
            return false;
        }
    }



    private void Movement_Performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        float speed = 2F;
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