using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class ThirdPersonController : MonoBehaviour
{

    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private Transform cameraTransform;

    private bool isGrounded;
    private float verticalVelocity;
    private CharacterController controller;
    private float turnSmoothVelocity;
    private Vector3 moveDirection;
    private CharacterStats stats;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * stats.getMovementSpeed() * Time.deltaTime);
        }

        if(isGrounded) {
            controller.Move(Vector3.down);
            if(!controller.isGrounded) {
                controller.Move(Vector3.up);
                isGrounded = false;
            }
        }

        // Jumping
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                verticalVelocity = stats.getJumpVelocity();
                isGrounded = false;
            } else {
                verticalVelocity = 0f;
            }
            controller.stepOffset = 1f;
        } else {
            controller.stepOffset = 0.1f;
        }

        verticalVelocity += Physics.gravity.y  * Time.deltaTime;
        Vector3 jumpVector = new Vector3(0f, verticalVelocity, 0f);
        controller.Move(jumpVector * Time.deltaTime);

        if(controller.isGrounded) {
            isGrounded = true;
        }
    }
}
