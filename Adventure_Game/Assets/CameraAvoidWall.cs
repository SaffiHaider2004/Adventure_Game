using UnityEngine;

public class CameraWallAvoider : MonoBehaviour
{
    public Transform playerHead; // Assign an empty GameObject at player's head
    public float minDistance = 0.5f;
    public float maxDistance = 4f;
    public float smoothSpeed = 10f;
    public LayerMask wallLayer;

    private Vector3 defaultLocalPos;

    void Start()
    {
        defaultLocalPos = transform.localPosition;
    }

    void LateUpdate()
    {
        if (!playerHead) return;

        Vector3 origin = playerHead.position;
        Vector3 desiredCameraPos = transform.position;

        Vector3 direction = desiredCameraPos - origin;
        float targetDistance = direction.magnitude;

        // Raycast from head to camera
        if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, maxDistance, wallLayer))
        {
            float hitDistance = Mathf.Clamp(hit.distance - 0.2f, minDistance, maxDistance);
            transform.position = Vector3.Lerp(transform.position, origin + direction.normalized * hitDistance, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // Return to default local position if nothing is blocking
            Vector3 targetWorldPos = playerHead.TransformPoint(defaultLocalPos);
            transform.position = Vector3.Lerp(transform.position, targetWorldPos, Time.deltaTime * smoothSpeed);
        }

        transform.LookAt(playerHead);
    }
}
