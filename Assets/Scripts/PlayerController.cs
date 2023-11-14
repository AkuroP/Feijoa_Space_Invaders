using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float limits;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    [Space]
    [SerializeField] private float _maxShootCD = 1f;
    private float _shootCD;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Shoot.performed += Shoot;
        playerInputActions.Player.PlayerMovement.performed += Move;
    }

    void Update()
    {
        Vector2 inputVector = playerInputActions.Player.PlayerMovement.ReadValue<Vector2>();
        transform.position += new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
        
        if (transform.position.x < -limits)
        {
            transform.position = new Vector3(-limits, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > limits)
        {
            transform.position = new Vector3(limits, transform.position.y, transform.position.z);
        }

        if(_shootCD > 0) _shootCD -= Time.deltaTime;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_shootCD > 0f) return;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            _shootCD = _maxShootCD;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        //     Vector2 inputVector = context.ReadValue<Vector2>();
        //     transform.position += new Vector3(inputVector.x * playerSpeed, 0, inputVector.y * playerSpeed);
        // }
    }
    
}
