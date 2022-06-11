using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class matches : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public mathmatch MMR;
    public Sprite endSprite, strSprite;
    public int KEY, pos;
    public bool isMoveable;

    private Vector2 lastMousePosition;
    private matches finded;

    [SerializeField] Sound sound;

    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isMoveable) return;

        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMoveable) return;

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
        if (!isMoveable) return;

        if (finded != null && finded.KEY == KEY)
        {
            sound.PlayTrueSound();

            finded.deactivate();
            deactivate();

            MMR.iDone();
        }
        else
            sound.PlayFalseSound();
        MMR.defPosition(pos);
    }

    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        if (!isMoveable) return true;

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
        finded = collision.gameObject.GetComponent<matches>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        finded = null;
    }

    public void deactivate()
    {
        enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Image>().sprite = endSprite;
    }

    public void resetMC()
    {
        enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Image>().sprite = strSprite;
        finded = null;
    }
}