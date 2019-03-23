﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        MoveOnTheGround();
        ApplyGravity();
        _characterController.Move(_moveDirection);
    }

    void MoveOnTheGround()
    {
        _moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
            Input.GetAxis(Axis.VERTICAL));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed * DeltaTime;
    }

    void ApplyGravity()
    {
        _verticalVelocity -= _gravity * DeltaTime;
        CheckIfPlayerWantsToJump();
        _moveDirection.y = _verticalVelocity * DeltaTime;
    }

    void CheckIfPlayerWantsToJump()
    {
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _verticalVelocity = jumpForce;
        }
    }

    private float DeltaTime
    {
        get
        {
            return Time.deltaTime;
        }
    }

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _gravity = 20f;
    private float _verticalVelocity;
    private float speed = 5f;
    private float jumpForce = 10f;
}
