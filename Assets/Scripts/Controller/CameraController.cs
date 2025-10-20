using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform objectToFollow;

    // Private fields to store the settings from the GameManager
    private Vector3 cameraOffset;
    private Vector3 lookOffset;
    private float moveSpeed;
    private float rotationSpeed;

    private void Start()
    {
        
        if (GameManager.instance != null)
        {
            cameraOffset = GameManager.instance.cameraOffset;
            lookOffset = GameManager.instance.lookOffset;
            moveSpeed = GameManager.instance.cameraMoveSpeed;
            rotationSpeed = GameManager.instance.cameraRotationSpeed;
        }
    }

    private void LateUpdate() // Switched to LateUpdate for camera movement
    {
        if (GameManager.instance.gameplayState.activeInHierarchy)
        {


            // 1. Check if we have a target to follow.
            if (objectToFollow == null)
            {
                // 2. If not, try to get it from the GameManager.
                objectToFollow = GameManager.instance.objectToFollow;

                // 3. If no objectToFollow do nothing this frame.
                if (objectToFollow == null)
                {
                    return;
                }
            }

            // Calculate the desired camera position relative to the target's rotation and position.
            Vector3 targetPosition = objectToFollow.position + objectToFollow.TransformDirection(cameraOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Calculate the point the camera should look at.
            Vector3 lookAtPosition = objectToFollow.position + objectToFollow.TransformDirection(lookOffset);
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Debug.Log("Camera current position: " + transform.position);
            Debug.Log("Camera target position: " + targetPosition);

            if (Input.GetKeyDown(KeyCode.O))
            {

            }
            if (Input.GetKeyDown(KeyCode.L))
            {

            }
        }
    }
}