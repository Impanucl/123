﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInputs : MonoBehaviour {
    [SerializeField] private CharacterController _charController;
    public float speed = 6.0f;
    [SerializeField] public Joystick moveJoystick;

    // Use this for initialization
    void Start () {
            _charController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update () {
        //for joystick
        //float deltaX = moveJoystick.Horizontal * speed;
        //float deltaZ = moveJoystick.Vertical * speed;

        //for keyboard
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;

        if (deltaX != 0 || deltaZ != 0)
        {
            Quaternion look = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * speed/5);
            _charController.Move(movement);
        }
        }
}
