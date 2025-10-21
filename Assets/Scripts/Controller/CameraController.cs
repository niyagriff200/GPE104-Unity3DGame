using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform objectToFollow;

    // Private fields to store the settings from the GameManager
    private Vector3 cameraOffset;
    private Vector3 lookOffset;
    private Vector3 offsetDirection;
    private float moveSpeed;
    private float rotationSpeed;
    private float currentOffsetDistance;
    private float minOffsetDistance;
    private float maxOffsetDistance;
    private float zoomSpeed;

    private void Start()
    {
        
        if (GameManager.instance != null)
        {
            cameraOffset = GameManager.instance.cameraOffset;
            lookOffset = GameManager.instance.lookOffset;
            moveSpeed = GameManager.instance.cameraMoveSpeed;
            rotationSpeed = GameManager.instance.cameraRotationSpeed;

            offsetDirection = cameraOffset.normalized;
            currentOffsetDistance = cameraOffset.magnitude;

            minOffsetDistance = GameManager.instance.cameraMinOffsetDistance;
            maxOffsetDistance = GameManager.instance.cameraMaxOffsetDistance;
            zoomSpeed = GameManager.instance.cameraZoomSpeed;

        }
    }

    private void LateUpdate() //Switched to LateUpdate for camera smoothness
    {
        if (GameManager.instance.gameplayState.activeInHierarchy)
        {
            if (objectToFollow == null)
            {
                objectToFollow = GameManager.instance.objectToFollow;
                if (objectToFollow == null)
                {
                    return;
                }
            }

            // Handle zoom input
            if (Input.GetKey(KeyCode.O))
            {
                currentOffsetDistance -= zoomSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.L))
            {
                currentOffsetDistance += zoomSpeed * Time.deltaTime;
            }
            currentOffsetDistance = Mathf.Clamp(currentOffsetDistance, minOffsetDistance, maxOffsetDistance);

            // Recalculate offset based on zoom
            Vector3 dynamicOffset = offsetDirection * currentOffsetDistance;

            // Apply dynamic offset to camera position
            Vector3 targetPosition = objectToFollow.position + objectToFollow.TransformDirection(dynamicOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Apply look offset
            Vector3 lookAtPosition = objectToFollow.position + objectToFollow.TransformDirection(lookOffset);
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}