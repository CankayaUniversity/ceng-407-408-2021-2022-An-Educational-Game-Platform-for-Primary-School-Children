using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BucketGame : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";
    public const string CONST_POS_TAG = "UIQuestionPos";
    public bool dragging = false;

    private const string EQUAL = "EqualBucket", SMALLER = "SmallerBucket", BIGGER = "BiggerBucket";

    private Vector2 orginalPosition, middlePos;
    private Transform corePanel;
    private Transform questionPos;
    private Transform objectToDrag;
    private Image objectToDragImage;

    private Transform equal, smaller, bigger;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    void Start()
    {
        corePanel = GameObject.Find("MainCanvas").transform.Find("GameCorePanel").transform;
        questionPos = corePanel.Find("CantMove").gameObject.transform;
        middlePos = questionPos.position;

        smaller = corePanel.Find("SmallerBucket").transform;
        equal = corePanel.Find("EqualBucket").transform;
        bigger = corePanel.Find("BiggerBucket").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
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
            if (objectToDrag != null)
            {
                Transform objectToCopy = GetQuestionPos();

                if (objectToCopy != null)
                {
                    objectToDrag.position = objectToCopy.position;
                    objectToCopy.position = orginalPosition; // doğru olunca yerine dönsün
                    //GameObject.Find("GT_GT_Manager").GetComponent<GradeThird_GameOne_Manager>().
                   this.GetComponent<GT_GameTwo_Manager>().chekForEquation(objectToDrag.gameObject.GetComponent<Image>().name);
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

    public void TurnOrginalPos(string typeName)
    {
        questionPos.position = middlePos;
        if(typeName == SMALLER)
        {
            smaller.position = orginalPosition;
        }
        else if(typeName == EQUAL)
        {
            equal.position = orginalPosition;
        }
        else //for bigger
        {
            bigger.position = orginalPosition;
        }
        
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

        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
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
