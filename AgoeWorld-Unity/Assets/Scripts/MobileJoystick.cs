using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [SerializeField] private RectTransform joystickArea;
    [SerializeField] private RectTransform joystickHandle;

    public Vector2 Direction { get; private set; }

    public void Initialize(RectTransform area, RectTransform handle)
    {
        joystickArea = area;
        joystickHandle = handle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (joystickArea == null || joystickHandle == null)
            return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickArea,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        float radius = Mathf.Min(
            joystickArea.rect.width,
            joystickArea.rect.height
        ) * 0.5f;

        if (radius <= 0f)
            return;

        Direction = Vector2.ClampMagnitude(localPoint / radius, 1f);
        joystickHandle.anchoredPosition = Direction * radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;

        if (joystickHandle != null)
            joystickHandle.anchoredPosition = Vector2.zero;
    }
}
