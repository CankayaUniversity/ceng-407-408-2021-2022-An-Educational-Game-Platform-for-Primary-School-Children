using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOptimize : MonoBehaviour
{
    public RectTransform scaledMain; // Other object big one
    private RectTransform tObject; //This object min one
    public float minWidth, minHeight, maxWidth, maxHeight;
    public float ratioX, ratioY; //Scale ratio
    public float childX, childY; //child width & height ratio
    private Vector2 size;
    int sign;
    public int moveChild;
    private void Start()
    {
        tObject = gameObject.GetComponent<RectTransform>();
        
        size = new Vector2(scaledMain.rect.width * ratioX, scaledMain.rect.height * ratioY);


        if (size.x < minWidth)
            size.x = minWidth;
        else if (size.x > maxWidth)
            size.x = maxWidth;

        if (size.y < minHeight)
            size.y = minHeight;
        else if (size.y > maxHeight)
            size.y = maxHeight;

        tObject.sizeDelta = size;

        sign = tObject.anchoredPosition.y > 0 ? 1 : -1;

        if(sign < 0)
            tObject.anchoredPosition = new Vector2(tObject.anchoredPosition.x, sign * tObject.rect.height / 2 - 100);
        else
            tObject.anchoredPosition = new Vector2(tObject.anchoredPosition.x, sign * tObject.rect.height / 2);

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            RectTransform child = gameObject.transform.GetChild(i).gameObject.GetComponent<RectTransform>();

            child.sizeDelta =
                new Vector2(size.x * childX, size.y * childY);

            child.anchoredPosition =
                new Vector2(moveChild * child.anchoredPosition.x
                    ,moveChild * tObject.anchoredPosition.y);
        }
    }
}
