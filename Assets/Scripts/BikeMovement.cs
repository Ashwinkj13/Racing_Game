using UnityEngine;

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
}