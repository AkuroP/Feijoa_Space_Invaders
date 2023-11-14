using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Instantiate(bulletPrefab);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        
    }
    
}
