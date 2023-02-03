using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody rb;

    private void Awake()
    {
        
    }
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}
