using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform target;

    [Header("Camera Follow")]
    public float smoothspeed = 5f;
    public float xoffset = 3f;
    public float yoffset = 0f;

    [Header("Camera Zoom")]
    public float normalSize = 5f;
    public float maxSize = 7f;
    public float maxSpeed = 20f;
    public float zoomSpeed = 3f;

    [Header("Speed Look Ahead")]
    public float maxLookAhead = 5f;
    public float lookAheadSpeed = 3f;

    private Camera cam;
    private Rigidbody2D targetRb;
    private float currentLookAhead = 0f;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (target != null)
        {
            targetRb = target.GetComponent<Rigidbody2D>();
        }

        cam.orthographicSize = normalSize;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        float speed = 0f;

        if (targetRb != null)
        {
            speed = Mathf.Abs(targetRb.linearVelocity.x);
        }

        float speedPercentage = Mathf.Clamp01(speed / maxSpeed);

        float targetLookAhead = maxLookAhead * speedPercentage;

        currentLookAhead = Mathf.Lerp(
            currentLookAhead,
            targetLookAhead,
            lookAheadSpeed * Time.deltaTime
        );

        float targetx = target.position.x + xoffset + currentLookAhead;
        float targety = target.position.y + yoffset;

        float newx = Mathf.Lerp(
            transform.position.x,
            targetx,
            smoothspeed * Time.deltaTime
        );

        float newy = Mathf.Lerp(
            transform.position.y,
            targety,
            smoothspeed * Time.deltaTime
        );

        transform.position = new Vector3(
            newx,
            newy,
            transform.position.z
        );

        float targetSize = Mathf.Lerp(
            normalSize,
            maxSize,
            speedPercentage
        );

        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize,
            targetSize,
            zoomSpeed * Time.deltaTime
        );
    }
}