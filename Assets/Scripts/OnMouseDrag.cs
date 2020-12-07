using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>For 2D, Canvas Drag and Drop Logic.</summary>
public class OnMouseDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Item self;
    RectTransform rect;

    void Awake()
    {
        self = GetComponent<Item>();
        rect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        rect.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");

        if (eventData.button == PointerEventData.InputButton.Left)
            GLOBAL.PlayingAs.Buy(self);
        else if (eventData.button == PointerEventData.InputButton.Right)
            GLOBAL.PlayingAs.Sell(self);
    }
}
