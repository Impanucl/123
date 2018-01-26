using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/MovePlayer")]
public class MovePlayer : MonoBehaviour {
    public float speed = 6.0f;
    public float rotationSpeed = 6.0f;
    public float gravity = -9.8f;
    public float mass = 100f;
    private CharacterController _charController;

	// Use this for initialization
	void Start () {
        _charController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
       float deltaY = Input.GetAxis("Horizontal") * rotationSpeed;
       float deltaX = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, 0);
        movement.y = gravity * mass;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

        _charController.transform.Rotate(0, deltaY, 0);
}
}
