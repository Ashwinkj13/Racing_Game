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

    private void OnValidate()
    {
        GenerateGround();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        spriteShapeController.spline.Clear();

        for (int i = 0; i < GroundLength; i++)
        {
            float x = i * XMultiplier;
            float noise = Mathf.PerlinNoise(i * noiseScale, 0f);
            float y = noise * YMultplier;

            Vector3 point = new Vector3(x, y, 0f);

            spriteShapeController.spline.InsertPointAt(
                i, point );

            if(i > 0 && i < GroundLength - 1)
            {
                spriteShapeController.spline.SetTangentMode(
                    i, ShapeTangentMode.Continuous);

                float tangentLength = XMultiplier * CurveSmoothness;

                spriteShapeController.spline.SetLeftTangent(
                    i, Vector3.left * tangentLength);

                spriteShapeController.spline.SetRightTangent(
                    i, Vector3.right * tangentLength);
            }
        }

        Vector3 bottomRight = new Vector3((GroundLength - 1) * XMultiplier, -BaseHeight, 0f);

        spriteShapeController.spline.InsertPointAt(GroundLength, bottomRight );

        Vector3 bottomleft = new Vector3(0f, -BaseHeight, 0f);

        spriteShapeController.spline.InsertPointAt(GroundLength + 1, bottomleft );

        spriteShapeController.spline.SetTangentMode(GroundLength, ShapeTangentMode.Linear);

        spriteShapeController.spline.SetTangentMode(GroundLength + 1, ShapeTangentMode.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
