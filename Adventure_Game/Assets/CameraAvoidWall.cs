using UnityEngine;

public class CameraWallBlocker : MonoBehaviour
{
    public Transform playerHead; // Usually an empty object at the head/shoulder
    public LayerMask wallLayer; // Assign wall/terrain layers here
    public float bufferDistance = 0.2f; // Keep a bit of distance from the wall
    public float smoothSpeed = 10f;

    private Vector3 defaultLocalPosition;

    void Start()
    {
        defaultLocalPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        if (!playerHead) return;

        Vector3 origin = playerHead.position;
        Vector3 desiredWorldPos = playerHead.TransformPoint(defaultLocalPosition);
        Vector3 direction = desiredWorldPos - origin;
        float distance = direction.magnitude;

        // Check if wall is blocking the view
        if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, distance, wallLayer))
        {
            // Pull camera to just before the wall
            float adjustedDistance = hit.distance - bufferDistance;
            adjustedDistance = Mathf.Max(adjustedDistance, 0.1f); // Never 0 to avoid inside player

            Vector3 newPos = origin + direction.normalized * adjustedDistance;
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // No obstruction — return to default local position
            transform.localPosition = Vector3.Lerp(transform.localPosition, defaultLocalPosition, Time.deltaTime * smoothSpeed);
        }

        transform.LookAt(origin + Vector3.up * 1.5f); // Optional: keeps framing on player
    }
}
