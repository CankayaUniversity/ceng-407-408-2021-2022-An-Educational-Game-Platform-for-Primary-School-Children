using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";
    public const string CONST_POS_TAG = "UIQuestionPos";
    public bool dragging = false;

    private Vector2 orginalPosition;

    private Transform objectToDrag;
    private Image objectToDragImage;
    private Transform clone;
    List<RaycastResult> hitObjects = new List<RaycastResult>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if(objectToDrag != null)
            {
                dragging = true;

                objectToDrag.SetAsLastSibling();

                orginalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(objectToDrag != null)
            {
                Transform objectToCopy = GetQuestionPos();

                if(objectToCopy != null)
                {
                    objectToDrag.position = objectToCopy.position;
                    objectToCopy.position = orginalPosition; // doğru olunca yerine dönsün
                    print(orginalPosition);
                    //GameObject.Find("GT_GT_Manager").GetComponent<GradeThird_GameOne_Manager>().
                    //TurnOrginalPos(objectToCopy);
                }
                else
                {
                    objectToDrag.position = orginalPosition;
                }

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
        }
    }

    public void TurnOrginalPos(Transform objectToCopy)
    {
        print("yer değiştirdiler");
        objectToCopy.position = objectToDrag.position;
        objectToDrag.position = orginalPosition;
    }

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects[0].gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        if(clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }

    private Transform GetQuestionPos()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        if (clickedObject != null && clickedObject.tag == CONST_POS_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }
}
