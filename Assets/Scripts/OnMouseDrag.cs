using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>For 2D, Canvas Drag and Drop Logic.</summary>
public class OnMouseDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler
{
    public TextMeshProUGUI ItemInfo;

    Item self;
    RectTransform rect;

    bool Dragging = false;

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
        Dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        Dragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Dragging)
            if (eventData.button == PointerEventData.InputButton.Left)
                GLOBAL.PlayingAs.Buy(self);
            else if (eventData.button == PointerEventData.InputButton.Right)
                GLOBAL.PlayingAs.Sell(self);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemInfo.text = self.ToString();
    }
}