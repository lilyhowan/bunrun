using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls player movement within the game
/// this includes long jumps, normal jumps and double jumps
/// the player controller also ends the game if the player falls out of the visible screen space
/// </summary>

public class PlayerController : MonoBehaviour
{
    // define constant variables
    public const float JumpForce = 15; // constant
    public const float JumpTime = 0.2f; // max time player can continue to jump while holding down button - constant

    // define public variables
    public float movementSpeed; 
    public bool isGrounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    // define managers
    public GameManager gameManager;

    // define animator
    private Animator playerAnimator;

    // speed increase variables
    public float speedMultiplier;
    public float speedIncreasePoint;
    private float speedIncreaseCount;
    public float maxMovementSpeedIncrease;

    // used to reset values at start of game
    private float movementSpeedStore;
    private float speedIncreaseCountStore;
    private float speedMultiplierStore;
    private float speedIncreasePointStore;

    // define private variables
    private Rigidbody2D playerRigidbody;
    //private Collider2D playerCollider; // catch all colliders (box, circle, etc)
    private float jumpTimeCounter;
    private bool canDoubleJump;

    // define sound variables
    public AudioSource jumpSound;


    void Start()
    {
        // find required components and set as variables e.g. get the animator component and set as the playerAnimator within this class
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        //playerCollider = GetComponent<Collider2D>();

        // store initial values so that they can be reset when the game ends
        jumpTimeCounter = JumpTime;
        speedIncreaseCount = speedIncreasePoint;
        movementSpeedStore = movementSpeed; 
        speedIncreaseCountStore = speedIncreaseCount;
        speedMultiplierStore = speedMultiplier;
        speedIncreasePointStore = speedIncreasePoint;
    }

    // each frame, check if input keys are being pressed - the player will jump if true
    void Update()
    {
         // set player to constantly move towards the right on the x axis via movementSpeed value
        playerRigidbody.velocity = new Vector2(movementSpeed, playerRigidbody.velocity.y);

        // check if player is touching ground
        //isGrounded = Physics2D.IsTouchingLayers(playerCollider, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // slowly increasing speed to increase difficulty of the game
        if (transform.position.x > speedIncreaseCount)
        {
            if (movementSpeed < maxMovementSpeedIncrease)
            {
                speedIncreaseCount += speedIncreasePoint;
                speedIncreasePoint = speedIncreasePoint * speedMultiplier; // increase speedIncreasePoint each time to increase distance/time between each
                movementSpeed = movementSpeed * speedMultiplier;
            }
        }

        // move player up on the y axis according to jumpForce value when up arrow, space or W keys are pressed
        // gravity from the Rigidbody 2D component will cause the player to eventually fall down
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W)))
        {
            if (isGrounded) // jump if isGrounded = true to prevent endless jumping
            {
                canDoubleJump = true;
                //Debug.Log("canDoubleJump = true");
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, JumpForce);
                jumpSound.Play();
            }
            if (!isGrounded && canDoubleJump)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, JumpForce);
                jumpTimeCounter = JumpTime;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

       // holding key down will cause the player to continue jumping for jumpTime duration
        if ((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.W)))
        {
            if (jumpTimeCounter > 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, JumpForce);
                jumpTimeCounter -= Time.deltaTime; // reducing counter per second
            }
        }

        // when the input key is lifted, reduce jumpTimeCounter to 0 therefore preventing the player from jumping midair
        if ((Input.GetKeyUp(KeyCode.UpArrow)) || (Input.GetKeyUp(KeyCode.Space)) || (Input.GetKeyUp(KeyCode.W)))
        {
            jumpTimeCounter = 0;
        }

        // when the player is grounded, reset the counter to the jumpTime value
        if (isGrounded)
        {
            jumpTimeCounter = JumpTime;
        }

        playerAnimator.SetFloat("Speed", playerRigidbody.velocity.x);
        playerAnimator.SetBool("Grounded", isGrounded);
    }

    // ends the game if the player falls out of the screen - boundary
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            //scoreEntry.Submit();
            gameManager.RestartGame(); // call function to restart game (resets all values)
            // reset movement values to stored numbers
            movementSpeed = movementSpeedStore;
            speedIncreaseCount = speedIncreaseCountStore;
            speedMultiplier = speedMultiplierStore;
            speedIncreasePoint = speedIncreasePointStore;
        }
    }

}
