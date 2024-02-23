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
    public bool isGrounded = true;
    public PlayerInput playerControls;
    private InputAction movementControls;
    private InputAction jumpControls;
    private InputAction possessControls;
    Vector2 moveDirection = Vector2.zero;

    public bool isPossessing;
    public string possessingTag;

    private void Awake()
    {
        playerControls = new PlayerInput();
        isPossessing = false;
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
    }

    void OnDisable()
    {
        movementControls.Disable();
        jumpControls.Disable();
        possessControls.Disable();
    }
    //required to set controls

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 7f;
        jumpStrength = 5.5f;
        //initiate vars
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();
        //update movement base on key inputs
        // if(!isGrounded)
        // {
        //     rb.AddForce
        // }
    }

    private void FixedUpdate()
    {
        if(isPossessing)
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        }
        else{
            rb.gravityScale = 0;
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
        //change velocity at fixed intervals allow flying as a ghost
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            Debug.Log("on");
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            Debug.Log("off");
        }
    }
    //collision ground checks may change with event manager

    private void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            Debug.Log("jumping activatied");
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
    }

    private void Possess(InputAction.CallbackContext context)
    {
        Debug.Log("posseses");
        if(!isPossessing)
        {
            isPossessing = true;
            Debug.Log("we posses now");
        }
        else{
            isPossessing = false;
            Debug.Log("we cool");
        }
    }
    //conects possess key to action
}
