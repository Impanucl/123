using UnityEngine;
using UnityEngine.EventSystems;
using Player;

namespace MYGUI{

	public class RotationJoystick : Joystick
	{
	    [Header("Options")]

	    Vector2 joystickCenter = Vector2.zero;

	    public float Xpos = 0;
	    public float Ypos = 0;

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

			setPositionRotationJoystick ();
	    }

	    public override void OnPointerDown(PointerEventData eventData)
	    {
            handle.gameObject.SetActive(true);
            Messenger<bool>.Broadcast(GameEvent.ON_TAP_ROTATION, background.gameObject.activeInHierarchy);
			Vector2 direction = eventData.position - joystickCenter;
			inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
			handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

			setPositionRotationJoystick ();
	    }

	    public override void OnPointerUp(PointerEventData eventData)
	    {
            handle.gameObject.SetActive(false);
            inputVector = Vector2.zero;
	        handle.anchoredPosition = Vector2.zero;
	        Messenger<bool>.Broadcast(GameEvent.ON_TAP_ROTATION, background.gameObject.activeInHierarchy);
	    }

		public void setPositionRotationJoystick()
		{
			//передача координат джойстика для анимации поворота оружия 
			Xpos = handle.transform.localPosition.x;
			Ypos = handle.transform.localPosition.y;
		}

	}
}