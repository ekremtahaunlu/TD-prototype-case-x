using UnityEngine;

public class CultOfLambCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 10f, -8f);
    public Vector3 lookAngle = new Vector3(40f, 0f, 0f);
    public float followSpeed = 5f;
    public float smoothTime = 0.3f;

    [Header("Boundaries (Optional)")]
    public bool useBoundaries = false;
    public Vector2 minBounds = new Vector2(-10f, -10f);
    public Vector2 maxBounds = new Vector2(10f, 10f);

    [Header("Camera Shake")]
    public float shakeIntensity = 0f;
    public float shakeDecay = 5f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 originalOffset;
    private float currentShakeIntensity = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerGO = GameObject.FindWithTag("Player");
            if (playerGO != null)
                player = playerGO.transform;
        }

        originalOffset = offset;
        transform.rotation = Quaternion.Euler(lookAngle);
    }

    void LateUpdate()
    {
        if (player == null) return;

        UpdateCameraPosition();
        HandleCameraShake();
    }

    void UpdateCameraPosition()
    {
        Vector3 targetPosition = player.position + offset;

        if (useBoundaries)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                minBounds.x + offset.x, maxBounds.x + offset.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z,
                minBounds.y + offset.z, maxBounds.y + offset.z);
        }

        transform.position = Vector3.SmoothDamp(transform.position,
            targetPosition, ref velocity, smoothTime);
    }

    void HandleCameraShake()
    {
        if (currentShakeIntensity > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * currentShakeIntensity;
            shakeOffset.z = shakeOffset.z * 0.5f;

            transform.position += shakeOffset;

            currentShakeIntensity -= shakeDecay * Time.deltaTime;
            currentShakeIntensity = Mathf.Max(0f, currentShakeIntensity);
        }
    }

    public void Shake(float intensity = 0.5f, float duration = 0.3f)
    {
        currentShakeIntensity = intensity;
        Invoke(nameof(StopShake), duration);
    }

    void StopShake()
    {
        currentShakeIntensity = 0f;
    }

    public void SetCameraAngle(Vector3 newAngle)
    {
        lookAngle = newAngle;
        transform.rotation = Quaternion.Euler(lookAngle);
    }

    public void SetZoom(float zoomLevel)
    {
        offset = originalOffset * zoomLevel;
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(player.position + offset, Vector3.one);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(player.position, player.position + offset);

            if (useBoundaries)
            {
                Gizmos.color = Color.red;
                Vector3 center = new Vector3(
                    (minBounds.x + maxBounds.x) / 2f,
                    0f,
                    (minBounds.y + maxBounds.y) / 2f);
                Vector3 size = new Vector3(
                    maxBounds.x - minBounds.x,
                    0.1f,
                    maxBounds.y - minBounds.y);
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}