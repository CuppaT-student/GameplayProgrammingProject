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
    //[SerializeField] public float speed = 2F;

    public bool isSprinting = false;
    
    [Header("Move Forces")]
    [SerializeField] public float moveForce = 1f;
    [SerializeField] public float walkForce = 1f;
    [SerializeField] public float runForce = 3f;
    [SerializeField] public float sprintForce = 2.0F;
    [SerializeField] private float maxSpeed = 3.0f;

    [SerializeField] private float jumpForce = 10.0F;
    private Vector3 forceDirection = Vector3.zero;
    [SerializeField] Camera playerCamera;
    private Animator animator;

    private float moveAmount;


    
    private void Awake()
    {
        // get the component - of this object instance - of the type rigidbody and assign it to our local ref
        capsuleRB = this.GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();

        animator = this.GetComponent<Animator>();
        
        /* old tutorial code
        playerInputActions.ThirdPersonPlayer.Enable();
        playerInputActions.ThirdPersonPlayer.Jump.performed += Jump;
        playerInputActions.ThirdPersonPlayer.Movement.performed += Movement_Performed;
        */
    }

    private void OnEnable()
    {
        playerInputActions.ThirdPersonPlayer.Jump.started += DoJump;
        playerInputActions.ThirdPersonPlayer.Attack.started += DoAttack;
        moveAction = playerInputActions.ThirdPersonPlayer.Movement;
        playerInputActions.ThirdPersonPlayer.Enable();
  
        playerInputActions.ThirdPersonPlayer.Sprint.started += DoSprint;
//        playerInputActions.ThirdPersonPlayer.Sprint.performed += i => isSprinting = true;

    }

    private void DoSprint(InputAction.CallbackContext obj)
    {
        Debug.Log("Sprint!");
        if (obj.performed)
        {
            isSprinting = true;
            Debug.Log("isSprint True!");
        }
        if (obj.canceled)
        {
            Debug.Log("Sprint False!");
            isSprinting = false;
        }
    }

    private void onDisable()
    {
        playerInputActions.ThirdPersonPlayer.Jump.started -= DoJump;
        playerInputActions.ThirdPersonPlayer.Attack.started -= DoAttack;
        playerInputActions.ThirdPersonPlayer.Disable();

        playerInputActions.ThirdPersonPlayer.Sprint.started -= DoSprint;
        //playerInputActions.ThirdPersonPlayer.Sprint.canceled += i => isSprinting = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        moveAmount = Mathf.Clamp01(Mathf.Abs(moveAction.ReadValue<Vector2>().x) + Mathf.Abs(moveAction.ReadValue<Vector2>().y));

        if (isSprinting && moveAmount >= 0.5F)
        {
            forceDirection += moveAction.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * sprintForce;
            forceDirection += moveAction.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * sprintForce;

        }
        else
        {
            if (moveAmount >= 0.5F)
            {
                forceDirection += moveAction.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * runForce;
                forceDirection += moveAction.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * runForce;
            }
            else
            {
                forceDirection += moveAction.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * walkForce;
                forceDirection += moveAction.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * walkForce;
            }
        }
        //        capsuleRB.velocity = forceDirection * 2.5f;
        capsuleRB.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        // create gravity accerlation 
        if (capsuleRB.velocity.y < 0f)
            capsuleRB.velocity -= Physics.gravity.y * Time.fixedDeltaTime * Vector3.down;

        // create a cap for the players speed but on the horizontal plane (so gravity still increases)
        Vector3 horizontalVelocity = capsuleRB.velocity;
        horizontalVelocity.y = 0;
        // check if we have exceeded our maxspeed variable
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            //if true, we set the horizontal speed to be the max and keep the current velocity in the y axis
            capsuleRB.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * capsuleRB.velocity.y;

        // Look at function controls the direction of our rb character
        LookAt();
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

        Debug.DrawLine(ray.origin, ray.origin + Vector3.down * 1.1f, Color.red, 2f);

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



    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        { 
            Debug.Log("Jump!" + context.phase);
            capsuleRB.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("attackTrigger");
    }
}




/*
private void Movement_Performed(InputAction.CallbackContext context)
{
    Debug.Log(context);
    Vector2 inputVector = context.ReadValue<Vector2>();
    capsuleRB.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
}
*/