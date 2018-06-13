using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : Joystick
{
    [Header("Options")]

    Vector2 joystickCenter = Vector2.zero;

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
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
		Debug.Log ('нажал');
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

}