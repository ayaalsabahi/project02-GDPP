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
    public Vector2 lastMoveDirection = Vector2.zero;

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
        boyMoveSpeed = 7f;
        boyJumpStrength = 15f;
        mouseMoveSpeed = 10f;
        mouseJumpStrength = 7f;
        moveSpeed = boyMoveSpeed;
        jumpStrength = boyJumpStrength;
        if(gameObject.name == "Mouse")
        {
            SetMouse();
        }
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
        //update facing dir
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
        // DrawLineInFront();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        rb.gravityScale = 3;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            Debug.Log("on");
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isOnWall = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            Debug.Log("off");
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
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
        Debug.Log("dropping");
        // Disable the collider component
        GetComponent<Collider2D>().enabled = false;

        // Start the coroutine to check and re-enable the collider when the GameObject isn't touching anything
        StartCoroutine(ReEnableColliderWhenNotTouching());
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

    public void SetMouse()
    {
        moveSpeed = mouseMoveSpeed;
        jumpStrength = mouseJumpStrength;
    }
}