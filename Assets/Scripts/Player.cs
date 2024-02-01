using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Vector2 inputVector;
    private Vector2 rotateVector;
    private Vector2 backPosition;
    [SerializeField] Vector2 maxPositionLimit;
    [SerializeField] GameObject body;
    [SerializeField] GameObject newBody;

    [SerializeField] private float time;
    [SerializeField] private float maxTime;

    void Start()
    {
        inputVector = Vector2.right;
    }

    void Update()
    {
        Move();
        eatFood();
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
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
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
        float castLength = 0.5f;
        LayerMask foodLayer = LayerMask.GetMask("Food");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, inputVector, castLength, foodLayer);

        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);

            if (newBody != null)
            {
                backPosition = (Vector2)newBody.transform.position + (-inputVector);
            }
            else
            {
                backPosition = (Vector2)transform.position + (-inputVector);
            }

            newBody = Instantiate(body, backPosition, Quaternion.identity);
            newBody.transform.parent = transform;
        }
    }
}
