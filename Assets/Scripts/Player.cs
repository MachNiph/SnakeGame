using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Vector2 inputVector;
    [SerializeField] Vector2 maxPositionLimit;

    [SerializeField] private float time;
    [SerializeField] private float maxTime;

    
    void Start()
    {
        inputVector = Vector2.right;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            inputVector = Vector2.up;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            inputVector = Vector2.down;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            inputVector = Vector2.left;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            inputVector = Vector2.right;
        }

        time -= Time.deltaTime;

        if (time <= 0 && canMove())
        {
            transform.position += (Vector3)inputVector;
            time = maxTime;
        }

    }

    private bool canMove()
    {
        bool canMove = ((transform.position.x < maxPositionLimit.x) &&
            (transform.position.x > -maxPositionLimit.x) && 
            (transform.position.y > -maxPositionLimit.y &&
            transform.position.y < maxPositionLimit.y));

        return canMove;
    }

    void eatFood()
    {

    }
}
