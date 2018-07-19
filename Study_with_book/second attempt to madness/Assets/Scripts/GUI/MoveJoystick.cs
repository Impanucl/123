using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI{

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
        Yposition = handle.transform.localPosition.y;
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

}
}