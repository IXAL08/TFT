using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doublejump : MonoBehaviour
{
    public PlayerMovement _playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //DobleSalto
        if (Input.GetKeyDown(KeyCode.E) && _playerMovement.doubleJump && _playerMovement._isGrounded == false)
        {
            _playerMovement._rb.velocity = Vector2.up * 5;
            _playerMovement.doubleJump = false;
        }
    }
}
