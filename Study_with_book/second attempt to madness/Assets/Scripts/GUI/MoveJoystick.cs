﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace MYGUI{

	public class MoveJoystick : Joystick
	{
	    [Header("Options")]

	    Vector2 joystickCenter = Vector2.zero;
	    public float Xposition = 0;
	    public float Yposition = 0;

	    private MoveJoystick moveJoystick;

	    void Start()
	    {
	        joystickCenter = background.position;
	        handle.gameObject.SetActive(false);
	        handle.anchoredPosition = Vector2.zero;
	    }

	    void Update()
	    {
	    
	    }

	    public override void OnDrag(PointerEventData eventData)
	    {
	        Vector2 direction = eventData.position - joystickCenter;
	        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
	        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

			setPositionMoveJoystick ();
	    }

	    public override void OnPointerDown(PointerEventData eventData)
	    {
            handle.gameObject.SetActive(true);
            Vector2 direction = eventData.position - joystickCenter;
			inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
			handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

			setPositionMoveJoystick ();
	    }

	    public override void OnPointerUp(PointerEventData eventData)
	    {
            handle.gameObject.SetActive(false);
	        inputVector = Vector2.zero;
	        handle.anchoredPosition = Vector2.zero;
	    }

		public void setPositionMoveJoystick()
		{
			//передача координат джойстика для анимации поворота оружия 
			Xposition = handle.transform.localPosition.x;
			Yposition = handle.transform.localPosition.y;
		}

	}
}