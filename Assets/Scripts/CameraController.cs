using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform objectToFollow;
    private Vector3 cameraOffset;
    private Vector3 lookOffset;
    private float moveSpeed;
    private float rotationSpeed;

    private void Start()
    {
        objectToFollow = GameManager.instance.objectToFollow;
        cameraOffset = GameManager.instance.cameraOffset;
        lookOffset = GameManager.instance.lookOffset;
        moveSpeed = GameManager.instance.cameraMoveSpeed;
        rotationSpeed = GameManager.instance.cameraRotationSpeed;
    }

    private void Update()
    {
        Vector3 newCameraPosition = objectToFollow.position + objectToFollow.TransformDirection(cameraOffset);
        transform.position = Vector3.MoveTowards(transform.position, newCameraPosition, moveSpeed * Time.deltaTime);

        Vector3 newCameraViewpoint = objectToFollow.position + objectToFollow.TransformDirection(lookOffset);
        Vector3 vectorFromCameraToTarget = newCameraViewpoint - transform.position;
        Quaternion endRotation = Quaternion.LookRotation(vectorFromCameraToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, rotationSpeed * Time.deltaTime);
    }
}
