using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] private bool _isGrounded, doubleJump = true;
    
    private float _horizontalInput, speed = 5.0f;
    public float jumpforce;
    private Rigidbody2D _rb;
    private bool facingRight = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //DobleSalto
        if (Input.GetKeyDown(KeyCode.E) && doubleJump && _isGrounded == false)
        {
            _rb.velocity = Vector2.up * 5;
            doubleJump = false;
        }
        
        //Salto
        if (Input.GetKey("space")&& _isGrounded)
        {
            if (jumpforce < 35)
            {
                jumpforce += 50 * Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp("space"))
        {
            _rb.AddForce(Vector3.up * jumpforce,ForceMode2D.Impulse);
            jumpforce = 0;
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
}
