using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private MobileJoystick mobileJoystick;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    public void SetMobileJoystick(MobileJoystick joystick)
    {
        mobileJoystick = joystick;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input;

        if (mobileJoystick != null)
        {
            input = mobileJoystick.Direction;
        }
        else
        {
            input = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
        }

        Vector3 movement =
            transform.right * input.x +
            transform.forward * input.y;

        movement = Vector3.ClampMagnitude(movement, 1f);

        controller.Move(
            movement * moveSpeed * Time.deltaTime
        );

        if (controller.isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = -2f;
        }

        verticalVelocity.y += gravity * Time.deltaTime;

        controller.Move(
            verticalVelocity * Time.deltaTime
        );
    }
}
