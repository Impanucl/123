using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : Joystick, FPSInputs
{
    [Header("Options")]

    Vector2 joystickCenter = Vector2.zero;
	private float Xposition = 0;
	private float Ypostion  = 0;

	private MoveJoystick moveJoystick;

    void Start()
    {
        joystickCenter = background.position;
        background.gameObject.SetActive(false);
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

		//передача координат джойстика для анимации движения плейера 
		Xposition = handle.transform.localPosition.x;
		Ypostion  = handle.transform.localPosition.y;
		SetPositionNumber(Xposition, Yposition);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }


	public float SetPositionNumber(float positionX, float positionY)
	{
	 



	}
}