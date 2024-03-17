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
    public float ghostMoveSpeed;
    public float ghostJumpStrength;
    public int ghostGrav = 0;
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
    public GameObject playerSwitcher;
    public PlayerSwitch playerSwitch;
    //Interacting
    public LayerMask interactableLayer;


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
        playerSwitch = playerSwitcher.GetComponent<PlayerSwitch>();
        ghostMoveSpeed = 7f;
        ghostJumpStrength = 13f;
        moveSpeed = ghostMoveSpeed;
        jumpStrength = ghostJumpStrength;
        //initiate vars
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = movementControls.ReadValue<Vector2>();
        //update movement base on key inputs
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
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
    }

//     IEnumerator Interact()
//    {
//        var facingDir = new Vector3(moveDirection.x,moveDirection.y);
//        var interactPos = transform.position + facingDir/6;


//        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
      
//        var collider = Physics2D.OverlapCircle(interactPos, .2f, interactableLayer);
//        if(collider != null)
//        {
//            yield return collider.GetComponent<Interactable>()?.Interact();
//        }


//    }

}