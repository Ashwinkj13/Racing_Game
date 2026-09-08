using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform target;

    [Header("Camera")]
    public float smoothspeed = 5f;
    public float xoffset = 3f;

    private float fixedY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixedY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            return;
        }
        float targetx = target.position.x + xoffset;

        float newx = Mathf.Lerp(
            transform.position.x,
            targetx,
            smoothspeed * Time.deltaTime
            );

        transform.position = new Vector3(
            newx,
            fixedY,
            transform.position.z
            );
    }
}
