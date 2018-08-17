using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUI;

namespace Player{

	public class FPSInputs : MonoBehaviour
	{
	    [SerializeField] private CharacterController _charController;
	    [SerializeField] public Joystick moveJoystick;

	    public float speed = 6.0f;
	    private float deltaX = 0;
	    private float deltaY = 0;
	    public int playerSection = 1;
	    public Sprite[] playerSprites = new Sprite[8];

	    //управление клавиатурой или джойстиком
	    public enum moveAxes
	    {
	        Joystick = 0,
	        Keyboard = 1
	    }

	    public moveAxes axes = moveAxes.Joystick;

	    void Start()
	    {
	        _charController = GetComponent<CharacterController>();
	    }

	    //движение персонажа
	    void Update()
	    {
	        MoveJoystick position = moveJoystick.GetComponent<MoveJoystick>();

	        if (axes == moveAxes.Joystick)
	        {
	            //управление джойстиком
	            deltaX = moveJoystick.Horizontal * speed;
	            deltaY = moveJoystick.Vertical * speed;

	            CalcPositionJoystick(position.Xposition, position.Yposition);
	        }
	        else
	        {
	            //управление клавиатурой
	            deltaX = Input.GetAxis("Horizontal") * speed;
	            deltaY = Input.GetAxis("Vertical") * speed;
	            CalcPositionKeyboard();
	        }

				//движение игрока
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

			//вычисление плоскости для поворота персонажа джойстиком
	    public void CalcPositionJoystick(float positionX, float positionY)
	    {

	        if ((positionX > 0) && (positionY > -positionX / 2) && (positionY < positionX / 2))
	        {
	            playerSection = 0;
	        }

	        if ((positionX > 0) && (positionY > positionX / 2) && (positionY < positionX * 2))
	        {
	            playerSection = 1;
	        }

	        if ((positionY > 0) && (positionX > -positionY / 2) && (positionX < positionY / 2))
	        {
	            playerSection = 2;
	        }

	        if ((positionY > 0) && (positionX > -positionY * 2) && (positionX < -positionY / 2))
	        {
	            playerSection = 3;
	        }

	        if ((positionX < 0) && (positionY > positionX / 2) && (positionY < -positionX / 2))
	        {
	            playerSection = 4;
	        }

	        if ((positionX < 0) && (positionY > positionX * 2) && (positionY < positionX / 2))
	        {
	            playerSection = 5;
	        }

	        if ((positionY < 0) && (positionX > positionY / 2) && (positionX < -positionY / 2))
	        {
	            playerSection = 6;
	        }

	        if ((positionY < 0) && (positionX > -positionY / 2) && (positionX < -positionY * 2))
	        {
	            playerSection = 7;
	        }

			GetComponent<SpriteRenderer>().sprite = playerSprites[playerSection];
	    }

		//вычисление плоскости для поворота персонажа клавиатурой
	    public void CalcPositionKeyboard()
	    {
	        if (Input.GetKey("d"))
	        {
	            playerSection = 0;
	        }

	        if (Input.GetKey("w"))
	        {
	            playerSection = 2;
	        }

	        if (Input.GetKey("a"))
	        {
	            playerSection = 4;
	        }

	        if (Input.GetKey("s"))
	        {
	            playerSection = 6;
	        }

	        if (Input.GetKey("d") && Input.GetKey("w"))
	        {
	            playerSection = 1;
	        }

	        if (Input.GetKey("w") && Input.GetKey("a"))
	        {
	            playerSection = 3;
	        }

	        if (Input.GetKey("a") && Input.GetKey("s"))
	        {
	            playerSection = 5;
	        }

	        if (Input.GetKey("s") && Input.GetKey("d"))
	        {
	            playerSection = 7;
	        }

			GetComponent<SpriteRenderer>().sprite = playerSprites[playerSection];
	    }
}
}
