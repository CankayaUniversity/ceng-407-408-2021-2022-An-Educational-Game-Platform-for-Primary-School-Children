using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class toplar : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public dondurma DR;
    public int KEY, order;

    private Vector2 lastMousePosition;

    private int finded;

    void Start()
    {
        finded = -1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (finded == KEY)
        {
            deactivate();
            DR.dogruAlan(order, KEY);
        }
        else DR.yanlisAlan(order);
    }

    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (int.TryParse(collision.name, out _)) finded = int.Parse(collision.name);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        finded = -1;
    }

    public void deactivate()
    {
        enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Text>().color = Color.green;
    }
}