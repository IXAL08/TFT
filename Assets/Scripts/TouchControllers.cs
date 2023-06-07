using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControllers : MonoBehaviour
{
    public Joystick joystick;
    public float velocidad;
    private float _horizontalInput, _verticalInput;
    public Rigidbody2D rb;
    private bool facingRight = true;

    [Header("Jump")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] public bool _isGrounded, doubleJump = true;
    [Range(1, 10)] public int JumpVelocity;
    public bool isJumping;
    
    [Header("Dashing")] 
    [SerializeField] private float dashingPower = 14f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 3.0f;
    private Vector2 _dashingDirection;
    public bool _isDashing, _canDash = true;
    private TrailRenderer _trailRenderer;
    
    
    [Header("Gancho")]
    private LineRenderer line;
    [SerializeField] private LayerMask grapplableMask;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float grappleSpeed = 10f;
    [SerializeField] private float grappleShootSpeed = 20f;
    [SerializeField] private bool isGrappling;
    [SerializeField] private float hookcooldown = 3.99f;
    [HideInInspector] public bool retracting;
    private Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
        line = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        //Vertical movement
        _verticalInput = joystick.Vertical;
        //horizontal movement
        _horizontalInput = joystick.Horizontal;
        rb.velocity = new Vector2(_horizontalInput * velocidad, rb.velocity.y);
        
        GroundCheck();
        if (Input.GetKeyDown(KeyCode.Q) && !isGrappling)
        {
           StartGrapple();
        }
                
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            rb.velocity += Vector2.up * JumpVelocity;
        }
        if (Input.GetKeyDown(KeyCode.Space) && doubleJump && _isGrounded == false)
        {
            rb.velocity = Vector2.up * 6;
            doubleJump = false;
        }
        var dashInput = Input.GetButtonDown("Dash");
        if (dashInput && _canDash)
        {
            StartCoroutine(StopDashing());
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            transform.position = grapplePos;
            line.SetPosition(0, transform.position);

            if (Vector2.Distance(transform.position,target) < 0.6f)
            {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Flip
        if (facingRight == false && _horizontalInput > 0)
        {
            Flip();
        } else if (facingRight && _horizontalInput < 0)
        {
            Flip();
        }
        
        if (retracting)
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    //Jump function
    public void Jump()
    {
        if (_isGrounded)
        {
            rb.velocity += Vector2.up * JumpVelocity;
            isJumping = true;
            Debug.Log("Salto");
        }
    }
    
    //DoubleJump Function
    public void DoubleJump()
    {
        if (doubleJump && _isGrounded == false)
        {
            rb.velocity = Vector2.up * 6;
            doubleJump = false;
            Debug.Log("Doble Salto");
        }
    }
    
    //Dash Function
    public void Dash()
    {
        if (_canDash)
        {
            StartCoroutine(StopDashing());
        }
    }
    
    //Hook function
    public void Hook()
    {
        if (!isGrappling)
        {
            StartGrapple();
        }
    }
    
    //SpriteFlip Function
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
    //GroundCheck Function
    private void GroundCheck()
    {
        _isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.3f, groundLayerMask);
        if (colliders.Length > 0)
        {
            _isGrounded = true;
            doubleJump = true;
            isJumping = false;
        }
    }
    //Hook iniciador
    private void StartGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, joystick.Direction, maxDistance, grapplableMask);

        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
        
    }
    
    //Dashing Enumerator
    private IEnumerator StopDashing()
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(_horizontalInput * dashingPower, _verticalInput * dashingPower);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        _trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        _isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        _canDash = true;
    }
    
    //hook Enumerator
    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1,transform.position);
        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0,transform.position);
            line.SetPosition(1,newPos);
            yield return null;
        }
        
        line.SetPosition(1,target);
        retracting = true;   
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("PlataformaMovible"))
        {
            transform.parent = col.transform;
        }
    
        if (col.gameObject.CompareTag("Pinchos"))
        {
            rb.AddForce(rb.transform.position * Vector2.down, ForceMode2D.Impulse);
        }
        if (col.gameObject.CompareTag("Pinchos"))
        {
            rb.AddForce(rb.transform.position * (Vector2.down * 3), ForceMode2D.Impulse);
        }

        if (col.gameObject.CompareTag("PinchosIzquierda"))
        {
            rb.AddForce(rb.transform.position * (Vector2.right * 3), ForceMode2D.Impulse);
        }
        
        if (col.gameObject.CompareTag("PinchosDerecha"))
        {
            rb.AddForce(rb.transform.position * (Vector2.left * -3), ForceMode2D.Impulse);
        }
    }
        
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("PlataformaMovible"))
        {
            transform.parent = null;
        }
    }
}
