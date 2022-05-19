using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saat : MonoBehaviour
{
    private Camera kamera;
    private Vector3 ekranAlaný;
    public BoxCollider2D[] hareketler;
    private float aciAra;
    public Text[] TXT;
    public GameObject mesaj;

    private Dictionary<string[], float> ANGLE;
    private Dictionary<string, float> MANGLE;

    public bool isStop = false;
    private float kalan = 120;
    private bool devam = true;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        kamera = Camera.main;

        ANGLE = new Dictionary<string[], float>();
        ANGLE.Add(new string[2] { "0", "12" }, 0);
        ANGLE.Add(new string[2] { "1", "13" }, 330);
        ANGLE.Add(new string[2] { "2", "14" }, 300);
        ANGLE.Add(new string[2] { "3", "15" }, 270);
        ANGLE.Add(new string[2] { "4", "16" }, 240);
        ANGLE.Add(new string[2] { "5", "17" }, 210);
        ANGLE.Add(new string[2] { "6", "18" }, 180);
        ANGLE.Add(new string[2] { "7", "19" }, 150);
        ANGLE.Add(new string[2] { "8", "20" }, 120);
        ANGLE.Add(new string[2] { "9", "21" }, 90);
        ANGLE.Add(new string[2] { "10", "22" }, 60);
        ANGLE.Add(new string[2] { "11", "23" }, 30);

        MANGLE = new Dictionary<string, float>();
        int kct = 1;
        for (int tck = 0; tck < 60; tck++)
        {
            kct -= 1;
            MANGLE.Add(kct.ToString(), tck * 6);
            if (kct == 0) kct = 60;
        }

        TXT[2].text = "K: " + kalan;
        string[] rastgeleKonumlar = new string[10] { "11:00","10:45", "09:30", "10:45", "12:45", "23:30", "10:00", "09:30", "10:00", "07:30" };

        TXT[0].text = rastgeleKonumlar[Random.Range(0, rastgeleKonumlar.Length - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!devam) return;

        Vector3 mousePozisyon = kamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (hareketler[0] == Physics2D.OverlapPoint(mousePozisyon))
            {
                ekranAlaný = kamera.WorldToScreenPoint(hareketler[0].transform.position);
                Vector3 v3 = Input.mousePosition - ekranAlaný;
                aciAra = (Mathf.Atan2(hareketler[0].transform.right.y, hareketler[0].transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;
            }
            else if (hareketler[1] == Physics2D.OverlapPoint(mousePozisyon))
            {
                ekranAlaný = kamera.WorldToScreenPoint(hareketler[1].transform.position);
                Vector3 v3 = Input.mousePosition - ekranAlaný;
                aciAra = (Mathf.Atan2(hareketler[1].transform.right.y, hareketler[1].transform.right.x) - Mathf.Atan2(v3.y, v3.x)) * Mathf.Rad2Deg;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (hareketler[0] == Physics2D.OverlapPoint(mousePozisyon))
            {
                Vector3 v3 = Input.mousePosition - ekranAlaný;
                float aci = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
                hareketler[0].transform.eulerAngles = new Vector3(0, 0, aci + aciAra);
            }
            else if (hareketler[1] == Physics2D.OverlapPoint(mousePozisyon))
            {
                Vector3 v3 = Input.mousePosition - ekranAlaný;
                float aci = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
                hareketler[1].transform.eulerAngles = new Vector3(0, 0, aci + aciAra);
            }
        }

        string[] hour = findNearPoint(hareketler[0].transform.eulerAngles.z);
        string minute = findNearMinute(hareketler[1].transform.eulerAngles.z);

        string temp = hour[0];
        if (int.Parse(temp) < 10) temp = "0" + temp;

        TXT[1].text = "AM: " + temp + ":" + minute + ", PM: " + hour[1] + ":" + minute;

        if (TXT[0].text.Equals(temp + ":" + minute) || TXT[0].text.Equals(hour[1] + ":" + minute))
        {
            TXT[0].color = Color.green;
            devam = false;
            giveMessage("TEBRIKLER DIGER BOLUME GECTIN");
        }
        else TXT[0].color = Color.red;
        if (isStop == false)
        {
            kalan -= Time.deltaTime;
            if (kalan <= 0)
            {
                devam = false;
                giveMessage("OYUNU BITIREMEDIN");
                TXT[2].text = "K: 0";
                TXT[2].color = Color.red;
                return;
            }
            TXT[2].text = "K: " + Mathf.RoundToInt(kalan);
        }
        
    }

    private string[] findNearPoint(float rot)
    {
        rot = Mathf.Abs(rot);
        while (rot > 360)
        {
            rot -= 360;
        }
        if (rot == 360) rot = 0;

        string[] located = new string[2] { "", "" };
        float nrpoint = 600;

        foreach (KeyValuePair<string[], float> KY in ANGLE)
        {
            float cal = Mathf.Abs(KY.Value - rot);
            
            if (nrpoint > cal)
            {
                nrpoint = cal;
                located = KY.Key;
            }
        }

        return located;
    }

    private string findNearMinute(float rot)
    {
        rot = Mathf.Abs(rot);
        while (rot > 360)
        {
            rot -= 360;
        }
        if (rot == 360) rot = 0;

        string located = "";
        float nrpoint = 600;
        foreach (KeyValuePair<string, float> KY in MANGLE)
        {
            float cal = Mathf.Abs(KY.Value - rot);

            if (nrpoint > cal)
            {
                nrpoint = cal;
                located = KY.Key; // tamamdýr // teþekkür ederim
            }
        }

        if (int.Parse(located) < 10) located = "0" + located;

        return located;
    }

    private void giveMessage(string msg)
    {
        mesaj.SetActive(true);
        TXT[3].text = msg;
    }
}