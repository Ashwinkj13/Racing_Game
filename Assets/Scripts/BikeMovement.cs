/*using UnityEngine;

public class BikeMovement : MonoBehaviour
{
    public float motorSpeed = -1000f;
    public float motorTorque = 10000f;

    private WheelJoint2D[] wheels;

    void Start()
    {
        wheels = GetComponentsInChildren<WheelJoint2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyGas();
        }
        else
        {
            RemoveGas();
        }
    }

    void ApplyGas()
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            JointMotor2D motor = wheel.motor;

            motor.motorSpeed = motorSpeed;
            motor.maxMotorTorque = motorTorque;

            wheel.motor = motor;
            wheel.useMotor = true;
        }
    }

    void RemoveGas()
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            wheel.useMotor = false;
        }
    }
}*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bike : MonoBehaviour
{
    public float motorspeed = 1000f;
    public float motortorque = 10000f;
    public float brakeTorque = 1000f;

    public float slowdownSpeed = 500f;

    private WheelJoint2D[] wheels;

    private bool accelerateButton;
    private bool brakeButton;

    public Image accelerateButtonimage;
    public Image breakButtonImage;

    public Sprite accelerateNormal;
    public Sprite acceleratePressed;

    public Sprite breakNormal;
    public Sprite breakPressed;

    void Start()
    {
        wheels = GetComponentsInChildren<WheelJoint2D>();
        accelerateButtonimage.sprite = accelerateNormal;
        breakButtonImage.sprite = breakNormal;
    }

    void Update()
    {
        // Keyboard controls
        bool accelerate = Input.GetKey(KeyCode.Space) || accelerateButton;
        bool brake = Input.GetKey(KeyCode.S) || brakeButton;

        if (accelerate)
        {
            ApplyGas();
        }
        else if (brake)
        {
            ApplyBrake();
        }
        else
        {
            NaturalSlowDown();
        }
    }

    void ApplyGas()
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            JointMotor2D motor = wheel.motor;

            motor.motorSpeed = -motorspeed;
            motor.maxMotorTorque = motortorque;

            wheel.motor = motor;
            wheel.useMotor = true;
        }
    }

    void ApplyBrake()
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            JointMotor2D motor = wheel.motor;

            motor.motorSpeed = 0f;
            motor.maxMotorTorque = brakeTorque;

            wheel.motor = motor;
            wheel.useMotor = true;
        }
    }

    /*void RemoveGas()
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            wheel.useMotor = false;
        }
    }*/

    // ACCELERATE BUTTON
    public void AccelerateDown()
    {
        accelerateButton = true;

        accelerateButtonimage.sprite = acceleratePressed;
    }

    public void AccelerateUp()
    {
        accelerateButton = false;
        accelerateButtonimage.sprite = accelerateNormal;
    }

    // BRAKE BUTTON
    public void BrakeDown()
    {
        brakeButton = true;
        breakButtonImage.sprite = breakPressed;
    }

    public void BrakeUp()
    {
        brakeButton = false;
        breakButtonImage.sprite = breakNormal;
    }

    void NaturalSlowDown()
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = Mathf.MoveTowards(motor.motorSpeed, 0f, slowdownSpeed * Time.deltaTime);
            wheel.motor = motor;
            wheel.useMotor = true;


        }
    }
}