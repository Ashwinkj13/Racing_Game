using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayDistance : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Distance;

    [SerializeField]
    private Transform playerTransform;

    private Vector2 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = (Vector2)playerTransform.position - startPosition;
        distance.y = 0f;
        if(distance.x < 0)
        {
            distance.x = 0;
        }
        Distance.text = distance.x.ToString("F0")+" m";
    }
}
