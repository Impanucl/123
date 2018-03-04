using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWeapon : MonoBehaviour
{
    public float speed = 6.0f;
    [SerializeField] public Joystick RotationJoystick;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = RotationJoystick.Horizontal * speed;
        float deltaZ = RotationJoystick.Vertical * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;

        if (RotationJoystick.Horizontal != 0 || RotationJoystick.Vertical != 0)
        {
            Quaternion look = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * speed / 5);
        }

    }
}
