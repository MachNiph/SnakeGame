using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

   
    private GameInput playerInputActions;


    private void Awake()
    {
        if(playerInputActions != null)
        {
            playerInputActions.Player.Enable();
        }
        
    }
    void Update()
    {
        GetMovement();
    }

    void GetMovement()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        transform.position += (Vector3)inputVector * 10 ;

    }
}
