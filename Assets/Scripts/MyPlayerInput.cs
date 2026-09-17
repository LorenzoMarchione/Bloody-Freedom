using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerInput : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 lookInput;

    private bool jump;
    private float verticalVelocity;
    private Vector2 currentMovement;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    private bool sprint;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private float cameraSensitivity = 1f;
    [SerializeField] private float movementSmooth =5f;
    private float cameraYaw;
    private float cameraPitch;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        
    }
    public void OnSprint(InputValue value)
    {
        sprint = value.isPressed;
    }
    public void OnJump(InputValue value)
    {
        jump = value.isPressed;
    }
    private void Update()
    {
        currentMovement = Vector2.Lerp(currentMovement,movement,10f * Time.deltaTime);
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            if (jump)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jump = false;
            }
        }

        verticalVelocity += gravity * Time.deltaTime;
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 direction = forward * currentMovement.y + right * currentMovement.x;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            float currentSpeed = sprint ? sprintSpeed : speed;

            characterController.Move(direction * currentSpeed * Time.deltaTime+ Vector3.up * verticalVelocity * Time.deltaTime
);
        }
    }
    private void LateUpdate()
    {
        if (cameraRoot == null)
            return;

        cameraYaw += lookInput.x * cameraSensitivity;
        cameraPitch -= lookInput.y * cameraSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -30f, 70f);

        cameraRoot.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

        transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);
    }
}
