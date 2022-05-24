using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private Text txt;
    private string emtpy = " ";

    public NumericCount numeric;
    private void Awake()
    {
        txt = this.transform.Find("Text").GetComponent<Text>();
        emtpy = numeric.emtpy;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (txt.text == emtpy)
            {
                txt.text = eventData.pointerDrag.transform.Find("Text").GetComponent<Text>().text;
                numeric.DestroyCevap(eventData.pointerDrag);
            }
        }
    }

    public void ToTheLine()
    {
        numeric.AddToTheLine(txt);
    }
}
