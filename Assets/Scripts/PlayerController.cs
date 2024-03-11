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
    [SerializeField]
    public Sprite ghostSprite;
    //Jump Logic
    public bool isGrounded = true;
    public bool isOnWall = true;
    //Inputs
    public PlayerInput playerControls;
    private InputAction movementControls;
    private InputAction jumpControls;
    private InputAction possessControls;
    //Possession
    public bool isPossessing;
    GameObject closestDoll;
    public DollController dollController;
    public string possessingTag;
    public bool isInDollRange;
    public float dollRange = .5f;
    [SerializeField]
    public LayerMask dollLayer;
    [SerializeField]
    public LineRenderer line;
    //Sprite and Animations
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerInput();
        isPossessing = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ghostSprite;
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
        DrawCircle();
        //debug purposes
        if(!isPossessing)
        {
            DollDetector();
        }
        else{
            
        }
    }

    private void FixedUpdate()
    {
        if(isPossessing)
        {
            rb.gravityScale = 3;
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        }
        else{
            rb.gravityScale = ghostGrav;
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
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isGrounded = true;
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
        if(!isPossessing && isInDollRange)
        {
            isPossessing = true;
            transform.position = dollController.transform.position;
            dollController.Hide();
            SetGhostProperties();
            Debug.Log("we posses now");
        }
        else{
            isPossessing = false;
            if(closestDoll != null)
            {
                closestDoll.transform.position = transform.position;
                closestDoll.GetComponent<DollController>().Show();
                closestDoll.GetComponent<SpriteRenderer>().enabled = true;
            }
            SetGhostProperties();
            Debug.Log("we cool");
        }
    }
    //conects possess key to action

    private void DollDetector()
    {
        // Define the radius for the overlap circle
        float detectionRadius = dollRange;

        // Get all colliders within the detection radius and on the dollLayer
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, detectionRadius, dollLayer);

        GameObject closestDoll = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Iterate through all hit colliders to find the closest doll
        foreach (var hitCollider in hitColliders)
        {
            GameObject doll = hitCollider.gameObject;
            float distance = (doll.transform.position - currentPosition).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDoll = doll;
                closestDistance = distance;
            }
        }
        if (closestDoll != null)
        {
            isInDollRange = true;
            dollController = closestDoll.GetComponent<DollController>();
            Debug.Log("touching");
        }
        else
        {
            isInDollRange = false;
            Debug.Log("not touching");
        }
    }

    private void DrawCircle()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false; // Ensure the LineRenderer positions are relative to the GameObject

        Vector2 center = Vector2.zero; // Center in local space
        float r = dollRange;
        int segments = 360;
        lineRenderer.positionCount = segments + 1; // Set the number of segments (plus one to close the circle)

        float deltaTheta = (2f * Mathf.PI) / segments; // Delta between each segment
        float theta = 0f;

        for (int index = 0; index <= segments; index++) // <= to close the circle
        {
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, y, 0) + (Vector3)center; // Convert Vector2 center to Vector3
            lineRenderer.SetPosition(index, pos);
            theta += deltaTheta;
        }
    }

    public void SetGhostProperties()
    {
        if(isPossessing)
        {
            moveSpeed = dollController.properties.moveSpeed;
            jumpStrength = dollController.properties.jumpStrength;
            rb.gravityScale = dollController.properties.gravity;
            spriteRenderer.sprite = dollController.properties.dollSprite;
        }
        else{
            moveSpeed = ghostMoveSpeed;
            jumpStrength = ghostJumpStrength;
            rb.gravityScale = ghostGrav;
            spriteRenderer.sprite = ghostSprite;  
        }
    }
}
