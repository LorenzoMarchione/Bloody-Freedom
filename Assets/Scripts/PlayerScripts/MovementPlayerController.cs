using UnityEngine;
using UnityEngine.InputSystem;


public class MovementPlayerController : MonoBehaviour
{
    private CharacterController controller;

    public float currentSpeed = 6f; // Make sure this is set to your desired speed if it was 0!

    public float gravity = -9.81f;

    [Header("Animation Ref")]
    [SerializeField] Animator animator;

    public Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Prevent errors if no keyboard is connected
        if (Keyboard.current == null) return;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Equivalent to Input.GetAxis("Horizontal") using A/D or Left/Right arrows
        float horizontal = 0f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal += 1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal -= 1f;

        // Equivalent to Input.GetAxis("Vertical") using W/S or Up/Down arrows
        float vertical = 0f;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical += 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical -= 1f;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", move.magnitude);
        animator.SetFloat("MovX", horizontal);
        animator.SetFloat("MovZ", vertical);



    }
}