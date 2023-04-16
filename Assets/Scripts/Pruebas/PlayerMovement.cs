using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Jump")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] public bool _isGrounded, doubleJump = true;
    [Range(1, 10)] public int JumpVelocity;
    public float _horizontalInput, speed = 5.0f;
    public Rigidbody2D _rb;
    private bool facingRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        //Salto
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.velocity += Vector2.up * JumpVelocity;

        }
   
    }

    private void FixedUpdate()
    {
        //Movimiento horizontal
        _horizontalInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);

        //Flip
        if (facingRight == false && _horizontalInput > 0)
        {
            Flip();
        } else if (facingRight && _horizontalInput < 0)
        {
            Flip();
        }
        //  GroundCheck
        GroundCheck();
    }
    
    //Funcion para el salto
    private void GroundCheck()
    {
        _isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f, groundLayerMask);
        if (colliders.Length > 0)
        {
            _isGrounded = true;
            doubleJump = true;
        }
    }
    
    //Cambio de dirección
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("PlataformaMovible"))
        {
            transform.parent = col.transform;
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
