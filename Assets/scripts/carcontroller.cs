using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carcontroller : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalinput;
    private float currentsteerangle;
    private float verticalinput;
    private float currentbreakForce;
    private bool isbreaking;
    [SerializeField] private float breakforce;
    [SerializeField] private float motorForce;
    [SerializeField] private float maxsteerangle;
    [SerializeField] private WheelCollider frontleftwheelcollider;
    [SerializeField] private WheelCollider frontrightwheelcollider;
    [SerializeField] private WheelCollider rearleftwheelcollider;
    [SerializeField] private WheelCollider rearrightwheelcollider;
    [SerializeField] private Transform frontleftwheeltransform;
    [SerializeField] private Transform frontrightwheeltransform;
    [SerializeField] private Transform rearleftwheeltransform;
    [SerializeField] private Transform rearrightwheeltransform;


    private void FixedUpdate()
    {
          Getinput();
          HandleMotor();
          HandleSteering();
          UpdateWheels();
        CheckIfCarFlipped();
    }
    private void HandleMotor()
    {
        frontleftwheelcollider.motorTorque = verticalinput * motorForce;
        frontrightwheelcollider.motorTorque = verticalinput * motorForce;
        currentbreakForce = isbreaking ? breakforce : 0f;
        ApplyBreaking();
    }
    private void ApplyBreaking()
    {
        frontleftwheelcollider.brakeTorque = currentbreakForce;
        frontrightwheelcollider.brakeTorque = currentbreakForce;
        rearleftwheelcollider.brakeTorque = currentbreakForce;
        rearrightwheelcollider.brakeTorque = currentbreakForce;
    }
    private void HandleSteering()
    {
        currentsteerangle = maxsteerangle * horizontalinput;
        frontleftwheelcollider.steerAngle = currentsteerangle;
        frontrightwheelcollider.steerAngle = currentsteerangle;
        frontrightwheelcollider.steerAngle = currentsteerangle;
    }
    private void Getinput()
     {
          horizontalinput=Input.GetAxis(HORIZONTAL);
          verticalinput = Input.GetAxis(VERTICAL);
          isbreaking = Input.GetKey(KeyCode.Space);
     }
    private void UpdateWheels()
    {
        updatesinglewheel(frontleftwheelcollider, frontleftwheeltransform);
        updatesinglewheel(frontrightwheelcollider, frontrightwheeltransform);
        updatesinglewheel(rearleftwheelcollider, rearleftwheeltransform);
        updatesinglewheel(rearrightwheelcollider, rearrightwheeltransform);



    }
    private void updatesinglewheel(WheelCollider wheelCollider,Transform wheeltransform )
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheeltransform.rotation = rot;
        wheeltransform.position = pos;
    }
    private void CheckIfCarFlipped()
    {
        // Check if the car is upside down (up vector not aligned with world up vector)
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            // Flip the car by 180 degrees on the Z-axis (yaw)
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z += 180f;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
