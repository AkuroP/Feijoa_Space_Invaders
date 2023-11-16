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

    [SerializeField]
    private Invaders_Manager _invadersManager;
    [Space]
    [Header("Player Health")]
    [SerializeField]
    private int _playerHP = 3;
    private float _maxRespawnCD = 3f;
    private float _respawnCD;
    private bool _respawning;


    //Get Set
    public bool Respawning { get => _respawning; set => _respawning = value; }
    public float RespawnCD { get => _respawnCD; set => _respawnCD = value; }
    public float MaxRespawnCD { get => _maxRespawnCD;}
    public float PlayerHP { get => _playerHP; }
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

    public void TakeDamage()
    {
        _playerHP--;
        if (_playerHP <= 0) return;
        if (_playerHP == 1) GameManager.instance.TurnVignetteColorTo(Color.red, true);
        _respawning = true;
        _respawnCD = _maxRespawnCD;

    }

    private void OnEnable() => _invadersManager.IsGamePlaying = true;

    private void OnDisable() => _invadersManager.IsGamePlaying = false;
}
