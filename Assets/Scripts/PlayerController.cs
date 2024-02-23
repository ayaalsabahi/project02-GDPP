using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpStrength;
    public bool canJump = true;
    public PlayerInput playerControls;
    private InputAction movementControls;
    private InputAction jumpControls;
    private InputAction interactControls;
    Vector2 moveDirection = Vector2.zero;

    public bool isPossessing;
    public string possessTag;

    private void Awake()
    {
        playerControls = new PlayerInput();
    }
    
    void OnEnable()
    {
        movementControls = playerControls.Ghost.Movement; 
        movementControls.Enable();

        jumpControls = playerControls.Ghost.Jump;
        jumpControls.Enable();
        jumpControls.performed += Jump;
    }

    void OnDisable()
    {
        movementControls.Disable();
        jumpControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5;
        jumpStrength = 5;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("jumping activatied");
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }
}
