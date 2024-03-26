using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Body Physics
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpStrength;
    public float groundDrag = 10;
    Vector2 moveDirection = Vector2.zero;
    private bool facingRight = true;
    //Ghost Properties
    public float boyMoveSpeed;
    public float boyJumpStrength;
    // [SerializeField]
    // public Sprite ghostSprite;
    //Jump Logic
    public bool isGrounded = true;
    public bool isOnWall;
    //Inputs
    public PlayerInput playerControls;
    private InputAction movementControls;
    private InputAction jumpControls;
    private InputAction possessControls;
    private InputAction interactControls;
    private InputAction dropControls;
    public GameObject playerSwitcher;
    public PlayerSwitch playerSwitch;
    //Interacting
    public LayerMask interactableLayer;
    public LayerMask groundLayer;
    public float groundCheckDistance;
    public float mouseGroundCheckDistance;
    public Vector2 lastMoveDirection = Vector2.zero;

    private Animator animator;

    public float mouseMoveSpeed;
    public float mouseJumpStrength;


    [Header("Events")]

    public GameEvent bodyPossesed;

    private void Awake()
    {
        playerControls = new PlayerInput();
        isOnWall = false;
    }

    
    void OnEnable()
    {
        movementControls = playerControls.Ghost.Movement; 
        movementControls.Enable();

        jumpControls = playerControls.Ghost.Jump;
        jumpControls.Enable();
        jumpControls.performed += Jump;

        possessControls = playerControls.Ghost.Possess;
        possessControls.Enable();
        possessControls.performed += Possess;

        interactControls = playerControls.Ghost.Interact;
        interactControls.Enable();
        interactControls.performed += Interact;

        dropControls = playerControls.Ghost.Drop;
        dropControls.Enable();
        dropControls.performed += Drop;
    }

    void OnDisable()
    {
        movementControls.Disable();
        jumpControls.Disable();
        possessControls.Disable();
        interactControls.Disable();
        dropControls.Disable();
    }
    //required to set controls

    // Start is called before the first frame update
    void Start()
    {
        playerSwitch = playerSwitcher.GetComponent<PlayerSwitch>();
        groundCheckDistance = 1.29f;
        boyMoveSpeed = 7f;
        boyJumpStrength = 15f;
        mouseMoveSpeed = 10f;
        mouseJumpStrength = 13f;
        mouseGroundCheckDistance = .5f;
        moveSpeed = boyMoveSpeed;
        jumpStrength = boyJumpStrength;
        if(gameObject.name == "Mouse")
        {
            SetMouse();
        }
        animator = GetComponentInChildren<Animator>();
        //initiate vars
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();
        //update movement base on key inputs
        if(moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }

        if (moveDirection.x < -0.01f && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (moveDirection.x >= 0.1f && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        isGrounded = GroundCheck();
        // isGrounded = true;

        if (animator) {
            animator.SetFloat("moveSpeed", Mathf.Abs(moveDirection.x));
            animator.SetBool("isGrounded", isGrounded);
        }

        //update facing dir
        if (isGrounded)
        {
            rb.drag = groundDrag;
            Debug.Log("on it");
        }
        else
        {
            rb.drag = 0;
            Debug.Log("off it");
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        rb.gravityScale = 3;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        // if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        // {
        //     isGrounded = true;
        //     Debug.Log("on");
        // }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isOnWall = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        // if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        // {
        //     isGrounded = false;
        //     Debug.Log("off");
        // }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isOnWall = false;
            Debug.Log("off");
        }
    }
    //collision ground checks may change with event manager

    private void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded || isOnWall)
        {
            Debug.Log("jumping activatied");
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
    }

    private void Possess(InputAction.CallbackContext context)
    {
        playerSwitch.SwitchPlayer();   
        Debug.Log("switching");
        bodyPossesed.Raise();
    }

    private void Drop(InputAction.CallbackContext context)
    {
        if(CheckForPlats())
        {
            Debug.Log("dropping");
            // Disable the collider component
            GetComponent<Collider2D>().enabled = false;

            // Start the coroutine to check and re-enable the collider when the GameObject isn't touching anything
            StartCoroutine(ReEnableColliderWhenNotTouching());
        }
    }

    IEnumerator ReEnableColliderWhenNotTouching()
    {
        // Wait for a short delay to allow the object to start falling through
        yield return new WaitForSeconds(.3f); // Adjust the delay time as needed

        // Continuously check if the GameObject is no longer touching anything
        while (true)
        {
            // If the GameObject's Rigidbody2D is not in contact with anything, re-enable the collider
            if (!rb.IsTouchingLayers())
            {
                GetComponent<Collider2D>().enabled = true;
                Debug.Log("Collider re-enabled");
                yield break; // Exit the coroutine
            }
            // Wait until the next fixed update to check again
            yield return new WaitForFixedUpdate();
        }
    }


    private void Interact(InputAction.CallbackContext context)
    {
        StartCoroutine(InteractCoroutine());
        Debug.Log("interacting");
    }


    IEnumerator InteractCoroutine()
    {
        Vector3 start = transform.position;
        // Calculate the interaction position based on the last move direction
        Vector3 interactPos = start + new Vector3(lastMoveDirection.x * 2, lastMoveDirection.y, 0) * 1.0f; // Adjust 1.0f to change the interaction distance

        // Draw a line from the player to the interaction position for visualization
        Debug.DrawLine(start, interactPos, Color.red, 1f);

        // Define the size of the box you want to use for the cast
        Vector2 boxSize = new Vector2(0.2f, 0.2f); // Adjust the size as needed

        // Calculate the direction from the start to the interaction position
        Vector2 direction = (interactPos - start).normalized;

        // Calculate the distance from the start to the interaction position
        float distance = Vector2.Distance(start, interactPos);

        // Perform a BoxCast from the start position in the direction of the interaction position
        RaycastHit2D hit = Physics2D.BoxCast(start, boxSize, 0f, direction, distance, interactableLayer);
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            // Perform the interaction
            yield return hit.collider.GetComponent<Interactable>()?.Interact();
        }
    }

    void DrawLineInFront()
    {
        Vector3 start = transform.position;
        // Use lastMoveDirection to ensure the line is drawn in the last known direction of movement
        Vector3 end = start + new Vector3(lastMoveDirection.x, lastMoveDirection.y, 0).normalized * 1.0f; // Adjust 1.0f to change the line length

        Debug.DrawLine(start, end, Color.blue, 0.01f); // Draw line for a very short duration to make it seem continuous
    }

    void DrawLineUnder()
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(0, -1f, 0).normalized * 1.0f; // Adjust 1.0f to change the line length

        Debug.DrawLine(start, end, Color.blue, 0.01f); // Draw line for a very short duration to make it seem continuous

    }

    public bool CheckForPlats()
    {
        Vector3 start = transform.position;
        // Adjusting the direction and length directly without normalizing and then multiplying, since it's a downward vector of fixed length
        Vector3 endPos = start + new Vector3(0, -1f, 0); 

        // Define the size of the box you want to use for the cast
        Vector2 boxSize = new Vector2(1.0f, 0.1f); // Adjust the width to match the player's width and a small height for the cast

        // Perform a BoxCast directly downwards from the start position
        RaycastHit2D hit = Physics2D.BoxCast(start, boxSize, 0f, Vector2.down, 1f, LayerMask.GetMask("Ground")); // Use the appropriate layer mask for your platforms

        if (hit.collider != null && hit.collider.CompareTag("Platform")) // Check if the hit collider has the "Platform" tag
        {
            Debug.Log("Standing on Platform: " + hit.collider.name);
            return true; // Standing on a platform
        }

        return false; // Not standing on a platform
    }

    public bool GroundCheck()
    {
        Vector2 position = transform.position; // Starting position of the ray
        Vector2 direction = Vector2.down; // Direction of the ray (downwards)
        
        // Cast a ray downward to check for ground
        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckDistance, groundLayer);

        // Optionally, draw the ray in the scene view for debugging
        Debug.DrawRay(position, direction * groundCheckDistance, Color.red);

        return hit.collider != null;
    }


    public void SetMouse()
    {
        moveSpeed = mouseMoveSpeed;
        jumpStrength = mouseJumpStrength;
        groundCheckDistance = mouseGroundCheckDistance;
    }
}