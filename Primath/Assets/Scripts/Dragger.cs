using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 dragOffset;
    private Vector3 startPos;
    private bool control = false;

    public void Start()
    {
        startPos = transform.position;
        PlayerPrefs.DeleteAll();
    }


    public void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    public void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    public void OnMouseUp()
    {
        if (!control)
        {
            transform.position = startPos;
        }
    }

    Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        return mousePos;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "1tlslot" && this.gameObject.tag == "1tl")
        {
            PlayerPrefs.SetInt("1tl",1);
            transform.position = collision.transform.position;
            control = true;
        }
        else if (collision.gameObject.tag == "50krsslot" && this.gameObject.tag == "50krs")
        {
            PlayerPrefs.SetInt("50krs", 1);
            transform.position = collision.transform.position;
            control = true;
        }
        else if (collision.gameObject.tag == "25krsslot" && this.gameObject.tag == "25krs")
        {
            PlayerPrefs.SetInt("25krs", 1);
            transform.position = collision.transform.position;
            control = true;
        }
        else if (collision.gameObject.tag == "10krsslot" && this.gameObject.tag == "10krs")
        {
            PlayerPrefs.SetInt("10krs", 1);
            transform.position = collision.transform.position;
            control = true;
        }
        else if (collision.gameObject.tag == "5krsslot" && this.gameObject.tag == "5krs")
        {
            PlayerPrefs.SetInt("5krs", 1);
            transform.position = collision.transform.position;
            control = true;
        }
        else if (collision.gameObject.tag == "1krsslot" && this.gameObject.tag == "1krs")
        {
            PlayerPrefs.SetInt("1krs", 1);
            transform.position = collision.transform.position;
            control = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "1tlslot" && this.gameObject.tag == "1tl")
        {
            PlayerPrefs.SetInt("1tl", 0);
            control = false;
        }
        else if (collision.gameObject.tag == "50krsslot" && this.gameObject.tag == "50krs")
        {
            PlayerPrefs.SetInt("50krs", 0);
            control = false;
        }
        else if (collision.gameObject.tag == "25krsslot" && this.gameObject.tag == "25krs")
        {
            PlayerPrefs.SetInt("25krs", 0);
            control = false;
        }
        else if (collision.gameObject.tag == "10krsslot" && this.gameObject.tag == "10krs")
        {
            PlayerPrefs.SetInt("10krs", 0);
            control = false;
        }
        else if (collision.gameObject.tag == "5krsslot" && this.gameObject.tag == "5krs")
        {
            PlayerPrefs.SetInt("5krs", 0);
            control = false;
        }
        else if (collision.gameObject.tag == "1krsslot" && this.gameObject.tag == "1krs")
        {
            PlayerPrefs.SetInt("1krs", 0);
            control = false;
        }
    }

}
