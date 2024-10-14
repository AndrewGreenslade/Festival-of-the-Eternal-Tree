using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;

    [Header("Movement attributes")]
    public float movementSpeed = 1;
    public float JumpForce = 100;
    public float FloorDetectionRange = 0.05f;

    [Header("Movement input")]
    public float xAxisInput;
    public float jumpInput;
    public bool PlayerGrounded = false;

    [Header("Other attributes")]
    public Transform floorDetectionRayTransform;
    public LayerMask floorLayerMask;

    void Awake()
    {   
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerGrounded = isGrounded();

        if (PlayerGrounded && jumpInput > 0)
        {
            playerRigidbody.AddForce(new Vector3(0, JumpForce, 0), ForceMode2D.Impulse);
            //trigger jump animation
            playerAnimator.SetBool("Jumping", true);
        }
        else if (PlayerGrounded) 
        { 
            //stop jump animation
            playerAnimator.SetBool("Jumping", false);
        }
    }

    void FixedUpdate()
    {
        //moves player on x axis with (input * movementSpeed)
        transform.position += new Vector3(xAxisInput, 0, 0) * movementSpeed * Time.fixedDeltaTime;

        //flip sprite if player is moving in negative direction on X axis
        playerSpriteRenderer.flipX = xAxisInput < 0 ?  true : false;

        //handle walking animations
        if (PlayerGrounded && xAxisInput != 0)
        {
            playerAnimator.SetBool("Walking", true);
        }
        else
        {
            playerAnimator.SetBool("Walking", false);
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(floorDetectionRayTransform.position, -transform.up, FloorDetectionRange, floorLayerMask);
        
        if(hit2D.collider != null)
        {
            Debug.DrawRay(floorDetectionRayTransform.position, -transform.up * FloorDetectionRange, Color.green);
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 0);
            return true;
        }
        else
        {
            Debug.DrawRay(floorDetectionRayTransform.position, -transform.up * FloorDetectionRange, Color.blue);
        }

        return false;
    }


    public void OnMove(InputValue value)
    {
        xAxisInput = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        jumpInput = value.Get<float>();
    }
}
