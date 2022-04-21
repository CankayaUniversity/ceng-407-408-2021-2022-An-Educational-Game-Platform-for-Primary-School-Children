using UnityEngine;

public class MoveImage : MonoBehaviour
{
    public int stepSize;
    Vector2 target;

    void Start()
    {
        target = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x,
            gameObject.GetComponent<RectTransform>().anchoredPosition.y);
    }
    public void Move()
    {
        target.x += stepSize;
    }

    private void Update()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = 
            Vector2.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition
            , target, Time.deltaTime * 2);
    }
}
