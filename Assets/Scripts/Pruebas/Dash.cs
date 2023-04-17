using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    public PlayerMovement _playerMovement;

    [Header("Dashing")] 
    [SerializeField] private float dashingPower = 14f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1.0f;
    private Vector2 _dashingDirection;
    public bool _isDashing, _canDash = true;
    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && _canDash)
        {
            StartCoroutine(StopDashing());
        }
                
    }

    private IEnumerator StopDashing()
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = _playerMovement._rb.gravityScale;
        _playerMovement._rb.gravityScale = 0f;
        _playerMovement._rb.velocity = new Vector2(_playerMovement._horizontalInput * dashingPower, Input.GetAxisRaw("Vertical") * dashingPower);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        _trailRenderer.emitting = false;
        _playerMovement._rb.gravityScale = originalGravity;
        _isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        _canDash = true;
    }
}
