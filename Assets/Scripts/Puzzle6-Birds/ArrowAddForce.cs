using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAddForce : MonoBehaviour
{
    Rigidbody rigidB;
    public float shootForce = 2000;
    
    void OnEnable()
    {
        rigidB = GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
        ApplyForce();
    }

    // Update is called once per frame
    void Update()
    {
        SpinObject();
    }

    void ApplyForce()
    {
        rigidB.AddRelativeForce(Vector3.right * shootForce);
    }

    void SpinObject()
    {
        //spin arrow
        float _yVelocity = rigidB.velocity.y;
        float _zVelocity = rigidB.velocity.z;
        float _xVelocity = rigidB.velocity.x;
        float _combinedVelocity = Mathf.Sqrt(_xVelocity * _xVelocity + _zVelocity * _zVelocity);
        float _fallAngle = Mathf.Atan2(_yVelocity, _combinedVelocity)*180/Mathf.PI;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, _fallAngle);
    }
}
