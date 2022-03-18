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
    private Animator animator;

    // create movement fields
    //[SerializeField] public float speed = 2F;
    private bool isSprinting = false;

    [Header("Required Components")]
    [SerializeField] Camera playerCamera;

    [Header("Move Forces")]
    //[SerializeField] public float moveForce = 1f;
    [SerializeField] public float walkForce = 1f;
    [SerializeField] public float runForce = 3f;
    private float sprintForce = 2.0F;
    [SerializeField] public float maxSpeed = 3.0f;
    private Vector3 forceDirection = Vector3.zero;
    private float moveAmount;



    [Header("Jump Values")]
    [SerializeField] private float jumpForce = 10.0F;
    [SerializeField] public bool canDoubleJump = false;
    [SerializeField] public bool hasJumped = false;
    [SerializeField] public bool hasDoubleJumped = false;

    [SerializeField] public bool triggerHeld = false;



    private void Awake()
    {
        // get the component - of this object instance - of the type rigidbody and assign it to our local ref
        capsuleRB = this.GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();

        animator = this.GetComponent<Animator>();

        playerInputActions.ThirdPersonPlayer.Trigger.performed += DoTrigger;
        playerInputActions.ThirdPersonPlayer.Trigger.canceled += DoTrigger;

    }

    private void OnEnable()
    {
        playerInputActions.ThirdPersonPlayer.Jump.started += DoJump;
        playerInputActions.ThirdPersonPlayer.Attack.started += DoAttack;
        moveAction = playerInputActions.ThirdPersonPlayer.Movement;
        playerInputActions.ThirdPersonPlayer.Trigger.performed += DoTrigger;
        playerInputActions.ThirdPersonPlayer.Trigger.canceled += DoTrigger;
        playerInputActions.ThirdPersonPlayer.Enable();
    }

    private void onDisable()
    {
        playerInputActions.ThirdPersonPlayer.Jump.started -= DoJump;
        playerInputActions.ThirdPersonPlayer.Attack.started -= DoAttack;
        playerInputActions.ThirdPersonPlayer.Trigger.performed -= DoTrigger;
        playerInputActions.ThirdPersonPlayer.Trigger.canceled -= DoTrigger;

        playerInputActions.ThirdPersonPlayer.Disable();
 
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
        // Jump Button Pressed Logs
        Debug.Log("doJump!");
       if(hasJumped && !canDoubleJump)
        {
            Debug.Log("In Air - Can't Double Jump!");
        }    
       else if (hasJumped && canDoubleJump)
        {
            Debug.Log("In Air - Performing Double Jump!");
        }

       // Jump Function 
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
            hasJumped = true;
        }
        else if(hasJumped && canDoubleJump)
        {
            forceDirection += Vector3.up * jumpForce;
            hasDoubleJumped = true; // Unused bool currently, but will be used for animation
            canDoubleJump = false;
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
            if (hasJumped)
            {
                hasJumped = false;
                if (hasDoubleJumped)
                {
                    hasDoubleJumped = false;
                }
            }
            
            return true;
        }
        else
        {
            Debug.Log("NOT GROUNDED!");
            return false;
        }
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("attackTrigger");
    }

    public void increaseMaxSpeed(float newSpeed, float time)
    {
        Debug.Log("increasing max speed!");
        maxSpeed += newSpeed;
        StartCoroutine(SpeedBuffTime(newSpeed, time));
        
    }
    IEnumerator SpeedBuffTime(float newSpeed, float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Resetting Max Speed!");
        maxSpeed -= newSpeed;
    }


    private void DoTrigger(InputAction.CallbackContext obj)
    {
        triggerHeld = obj.ReadValueAsButton();
        Debug.Log("Trigger Pressed");
    }
}
