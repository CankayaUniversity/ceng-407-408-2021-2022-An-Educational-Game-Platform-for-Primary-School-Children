using UnityEngine;

public class MoveImage : MonoBehaviour
{
    public int stepSize;
    public GameObject targetGO;
    Vector2 moveTarget;
    Vector2 target;

    public bool isTargetMoving;
    public GradeThird_GameOne_Manager gradeThird;

    private bool isFinished;
    void Start()
    {
        isFinished = false;
        moveTarget = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x,
            gameObject.GetComponent<RectTransform>().anchoredPosition.y);
        target = targetGO.GetComponent<RectTransform>().anchoredPosition;
    }
    public void Move()
    {
        moveTarget.x += stepSize;
        if(moveTarget.x >= target.x - 5f)
        {
            isFinished = true;
        }
    }

    private void Update()
    {
        if (isTargetMoving)
        {
            target = targetGO.GetComponent<RectTransform>().anchoredPosition;
        }
        if (isFinished)
        {
            gradeThird.Finish();
        }
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition
            , moveTarget, Time.deltaTime * 2);
    }
}
