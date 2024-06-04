using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    private float moveVector;
    private bool _drag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _drag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveVector = eventData.delta.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _drag = false;
    }

    public float GetVertical()
    {
        return moveVector;
    }

    public bool GetDrag()
    {
        return _drag;
    }
}