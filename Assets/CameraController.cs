using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraDistance = 10.0f;
    public float cameraHeight = 5.0f;
    public float cameraRotationSpeed = 3.0f;
    public float cameraReturnSpeed = 3.0f;
    public float cameraMinYAngle = -60.0f;
    public float cameraMaxYAngle = 60.0f;
    public LayerMask cameraCollisionMask;
    public Vector2 sensitivity;

    private Vector3 offset;
    private float currentXAngle = 0.0f;
    private float currentYAngle = 0.0f;

    void Start()
    {
        offset = transform.position - playerTransform.position;
        currentXAngle = transform.eulerAngles.x;
        currentYAngle = transform.eulerAngles.y;
    }

    void OnPreRender()
    {
        // move camera with player
        transform.position = playerTransform.position + offset;

        // rotate camera around player with mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity.y;
        currentXAngle -= mouseY * cameraRotationSpeed;
        currentXAngle = Mathf.Clamp(currentXAngle, cameraMinYAngle, cameraMaxYAngle);
        currentYAngle += mouseX * cameraRotationSpeed;
        Quaternion rotation = Quaternion.Euler(currentXAngle, currentYAngle, 0);
        Vector3 direction = rotation * Vector3.back;
        transform.position = playerTransform.position + direction * cameraDistance;
        transform.LookAt(playerTransform.position + Vector3.up * cameraHeight);
    }
}
