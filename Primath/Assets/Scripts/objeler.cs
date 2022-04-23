using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class objeler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public tren TR;
    public int myOrder;

    private Vector2 lastMousePosition;

    private Text benimDeger;

    private int degen;

    void Start()
    {
        degen = -1;
        benimDeger = transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    /// <summary>
    /// This method will be called on the start of the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    /// <summary>
    /// This method will be called during the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
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
        if (degen == int.Parse(benimDeger.text))
        {
            deactivate();
            TR.dogruAlan(int.Parse(benimDeger.text) - 1);
        }
        else TR.yanlisAlan(myOrder);
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

    public void deactivate()
    {
        enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Image>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        degen = int.Parse(collision.transform.GetChild(0).GetComponent<Text>().text);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        degen = -1;
    }
}