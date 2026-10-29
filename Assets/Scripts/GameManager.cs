using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private Image Fuelimage;

    [SerializeField, Range(0.1f, 5f)]
    private float FadeSpeed = 1f;

    [SerializeField]
    private float maxFuel = 100f;

    [SerializeField]
    private Gradient fuel;


    private float currentFuel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        currentFuel -= Time.deltaTime * FadeSpeed;
        UpdateUI();
        if (currentFuel <= 0)
        {
            GameRestart.Instance.GameOver();
        }
    }

    private void UpdateUI()
    {
        Fuelimage.fillAmount = (currentFuel / maxFuel);
        Fuelimage.color = fuel.Evaluate(Fuelimage.fillAmount);
    }

    public void FillFuel()
    {
        Debug.Log(currentFuel);
        currentFuel = maxFuel;
        UpdateUI() ;
    }
}
