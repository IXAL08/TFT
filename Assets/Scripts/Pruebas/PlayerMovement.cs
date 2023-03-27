using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] private bool _isGrounded, doubleJump;
    
    private float _horizontalInput, speed = 5.0f, force;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento horizontal
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (_horizontalInput * speed * Time.deltaTime));
        //Salto
        if (Input.GetKey("space")&& _isGrounded)
        {
            if (force < 30)
            {
                force += 30 * Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp("space"))
        {
            _rb.AddForce(transform.up * force,ForceMode2D.Impulse);
            if (doubleJump)
            {
                _rb.AddForce(transform.up * 80,ForceMode2D.Impulse);
                doubleJump = false;
            }
            force = 0;
        }
    }

    private void FixedUpdate()
    {
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
}
