using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("The transform of the player to follow")]
    public Transform targetToFollow;

    [Header("Position Settings")]
    [Tooltip("The height from the target on the Y axis")]
    public float height = 10f;
    [Tooltip("The distance from the target in the XZ plane")]
    public float distance = 10f;

    [Header("Follow Settings")]
    [Tooltip("How quickly the camera follows the target (higher = faster)")]
    [Range(0.01f, 10f)]
    public float followSpeed = 5f;
    [Tooltip("Use smooth follow or instant follow")]
    public bool smoothFollow = true;

    [Header("Camera Angle")]
    [Tooltip("Rotation on the X axis (tilt)")]
    [Range(0f, 90f)]
    public float xRotation = 45f;
    [Tooltip("Rotation on the Y axis (yaw)")]
    [Range(-180f, 180f)]
    public float yRotation = 45f;

    private Vector3 targetPosition;

    private void Start()
    {
        // Check if target is assigned
        if (targetToFollow == null)
        {
            Debug.LogError("IsometricCamera: No target assigned! Please assign a target in the Inspector.");
            enabled = false;
            return;
        }

        // Set initial position and rotation
        UpdateCameraTransform();
    }

    private void LateUpdate()
    {
        if (targetToFollow == null)
            return;

        // Calculate the desired position
        CalculateCameraPosition();

        // Move the camera to the target position
        if (smoothFollow)
        {
            // Smoothly interpolate between the camera's current position and the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            // Instantly move to the target position
            transform.position = targetPosition;
        }

        // Always look at the target
        transform.LookAt(targetToFollow);
    }

    private void CalculateCameraPosition()
    {
        // Start with the target's position
        Vector3 pos = targetToFollow.position;

        // Calculate horizontal offset based on the yRotation
        float xzOffset = Mathf.Sin(yRotation * Mathf.Deg2Rad) * distance;
        float zxOffset = Mathf.Cos(yRotation * Mathf.Deg2Rad) * distance;

        // Apply the offsets
        pos.x -= xzOffset;
        pos.y += height;
        pos.z -= zxOffset;

        targetPosition = pos;
    }

    private void UpdateCameraTransform()
    {
        // Calculate the desired position
        CalculateCameraPosition();

        // Set the position
        transform.position = targetPosition;

        // Make sure the camera is looking at the target
        transform.LookAt(targetToFollow);
    }

    // Called when values are changed in the inspector
    private void OnValidate()
    {
        // Update position and rotation when values are changed in the inspector during play mode
        if (Application.isPlaying && targetToFollow != null)
        {
            UpdateCameraTransform();
        }
    }

    // A method that can be called to change the target at runtime
    public void SetNewTarget(Transform newTarget)
    {
        targetToFollow = newTarget;
        enabled = true;

        // Update position and rotation for the new target
        if (targetToFollow != null)
        {
            UpdateCameraTransform();
        }
    }
}