using UnityEngine;

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
        _moveDirection = new Vector3(Input.GetAxis(Axis.Horizontal), 0f,
            Input.GetAxis(Axis.Vertical));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed * TimeUtils.DeltaTime;
    }

    void ApplyGravity()
    {
        _verticalVelocity -= _gravity * TimeUtils.DeltaTime;
        CheckIfPlayerWantsToJump();
        _moveDirection.y = _verticalVelocity * TimeUtils.DeltaTime;
    }

    void CheckIfPlayerWantsToJump()
    {
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _verticalVelocity = jumpForce;
        }
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _gravity = 20f;
    private float _verticalVelocity;
    private float speed = 5f;
    private float jumpForce = 10f;
}
