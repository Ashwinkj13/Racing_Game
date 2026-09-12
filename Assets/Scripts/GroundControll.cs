using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class GroundControll : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;

    [Header("Ground settings")]
    [Range(5, 200)]
    public int GroundLength = 100;

    public float XMultiplier = 3f;
    public float YMultplier = 5f;
    public float noiseScale = 0.1f;

    [Range(0f, 1f)]
    public float CurveSmoothness = 0.5f;

    public float BaseHeight = 10f;

    [Header("Ground Joining")]
    [Range(1, 20)]
    public int TransitionPoints = 5;

    private void OnValidate()
    {
        GenerateGround();
    }

    void Start()
    {
        GenerateGround();
    }

    public void GenerateGround()
    {
        if (spriteShapeController == null)
        {
            spriteShapeController = GetComponent<SpriteShapeController>();
        }

        if (spriteShapeController == null)
        {
            Debug.LogError("GroundController: spriteController not found!");
            return;
        }

        // Find the ground immediately to the left
        GroundControll previousGround = FindPreviousGround();

        float startHeight = 0f;

        if (previousGround != null)
        {
            int requiredPoints = previousGround.GroundLength + 2;

            int previousPointCount =
                previousGround.spriteShapeController.spline.GetPointCount();

            // Make sure the previous ground has actually generated
            if (previousPointCount >= requiredPoints)
            {
                int lastSurfacePoint =
                    previousGround.GroundLength - 1;

                // Get the previous ground's final surface point
                Vector3 previousLocalPoint =
                    previousGround.spriteShapeController.spline.GetPosition(
                        lastSurfacePoint
                    );

                // Convert previous point to world space
                Vector3 previousWorldPoint =
                    previousGround.transform.TransformPoint(
                        previousLocalPoint
                    );

                // Convert that world point into THIS ground
                // so we can use its local Y value
                Vector3 currentLocalPoint =
                    transform.InverseTransformPoint(
                        previousWorldPoint
                    );

                startHeight = currentLocalPoint.y;
            }
        }

        spriteShapeController.spline.Clear();

        for (int i = 0; i < GroundLength; i++)
        {
            float x = i * XMultiplier;

            float noise = Mathf.PerlinNoise(
                i * noiseScale,
                0f
            );

            float normalY = noise * YMultplier;

            float y = normalY;

            // Smoothly connect to the previous ground
            if (previousGround != null && i < TransitionPoints)
            {
                float t = (float)i / (TransitionPoints - 1);

                y = Mathf.Lerp(
                    startHeight,
                    normalY,
                    t
                );
            }

            Vector3 point = new Vector3(
                x,
                y,
                0f
            );

            spriteShapeController.spline.InsertPointAt(
                i,
                point
            );

            if (i > 0 && i < GroundLength - 1)
            {
                spriteShapeController.spline.SetTangentMode(
                    i,
                    ShapeTangentMode.Continuous
                );

                float tangentLength =
                    XMultiplier * CurveSmoothness;

                spriteShapeController.spline.SetLeftTangent(
                    i,
                    Vector3.left * tangentLength
                );

                spriteShapeController.spline.SetRightTangent(
                    i,
                    Vector3.right * tangentLength
                );
            }
        }

        Vector3 bottomRight = new Vector3(
            (GroundLength - 1) * XMultiplier,
            -BaseHeight,
            0f
        );

        spriteShapeController.spline.InsertPointAt(
            GroundLength,
            bottomRight
        );

        Vector3 bottomLeft = new Vector3(
            0f,
            -BaseHeight,
            0f
        );

        spriteShapeController.spline.InsertPointAt(
            GroundLength + 1,
            bottomLeft
        );

        spriteShapeController.spline.SetTangentMode(
            GroundLength,
            ShapeTangentMode.Linear
        );

        spriteShapeController.spline.SetTangentMode(
            GroundLength + 1,
            ShapeTangentMode.Linear
        );
    }

    GroundControll FindPreviousGround()
    {
        GroundControll[] allGrounds =
            FindObjectsByType<GroundControll>(
                FindObjectsSortMode.None
            );

        GroundControll previous = null;

        float currentX = transform.position.x;

        foreach (GroundControll ground in allGrounds)
        {
            if (ground == this)
            {
                continue;
            }

            float groundX = ground.transform.position.x;

            if (groundX < currentX)
            {
                if (previous == null ||
                    groundX > previous.transform.position.x)
                {
                    previous = ground;
                }
            }
        }

        return previous;
    }

    void Update()
    {
    }
}