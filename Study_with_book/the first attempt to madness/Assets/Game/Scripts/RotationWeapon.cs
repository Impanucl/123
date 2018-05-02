using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWeapon : MonoBehaviour
{
	public float speed = 6.0f;

	public enum RotationAxes { 
		Joystick = 0,
		Mouse = 1
	};

	public RotationAxes axes = RotationAxes.Joystick;

	//-----------joystick---------
    [SerializeField] public Joystick RotationJoystick;


	//------------mouse-----------
	Vector3 mousePosition;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

		if (axes == RotationAxes.Joystick) {
			float deltaX = RotationJoystick.Horizontal * speed;
			float deltaZ = RotationJoystick.Vertical * speed;
			Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
			movement = Vector3.ClampMagnitude (movement, speed);
			movement *= Time.deltaTime;

			if (RotationJoystick.Horizontal != 0 || RotationJoystick.Vertical != 0) {
				Quaternion look = Quaternion.LookRotation (movement);
				transform.rotation = Quaternion.Slerp (transform.rotation, look, Time.deltaTime * speed / 5);
			}
		}else if (axes == RotationAxes.Mouse)
			
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
		Vector3 difference = mousePosition - transform.position; 
		difference.Normalize();
		float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, rotation_z, 0f); 


		Debug.Log (difference);


		/*

		// Тот самый поворот
		// вычисляем разницу между текущим положением и положением мыши
		Vector3 difference = mousePosition2 - transform.position; 
		difference.Normalize();
		// вычисляемый необходимый угол поворота
		float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		// Применяем поворот вокруг оси Z
		//transform.rotation = Quaternion.Euler(0f, rotation_z, 0f);    
*/
    }
		
}
