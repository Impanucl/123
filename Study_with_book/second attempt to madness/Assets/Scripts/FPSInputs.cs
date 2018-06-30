using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInputs : MonoBehaviour {
	[SerializeField] private CharacterController _charController;
	public float speed = 6.0f;
	[SerializeField] public Joystick moveJoystick;

	private float deltaX = 0;
	private float deltaY = 0;

	public enum moveAxes
	{
		Joystick = 0,
		Keyboard = 1
	}

	public moveAxes axes = moveAxes.Joystick;

	void Start () {
		_charController = GetComponent<CharacterController>();
	}
		
	void Update () {
		
		if (axes == moveAxes.Joystick) {
			//управление джойстиком
			 deltaX = moveJoystick.Horizontal * speed;
			 deltaY = moveJoystick.Vertical * speed;
		} else {
			//управление клавиатурой
			 deltaX = Input.GetAxis("Horizontal") * speed;
			 deltaY = Input.GetAxis("Vertical") * speed;
		}

		Vector3 movement = new Vector3(deltaX, deltaY, 0);
		movement = Vector3.ClampMagnitude(movement, speed);
		movement *= Time.deltaTime;

		if (deltaX != 0 || deltaY != 0)
		{
			//эти строки позволяют при движении поворачивать корпус плейера, для 3d 
			//	Quaternion look = Quaternion.LookRotation(movement);
			//	transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * speed/5);
			_charController.Move(movement);
		}
	}

	//вычисление 
	public float SetPlayerPosition(float Ox, float Oy){
			
	}

}
