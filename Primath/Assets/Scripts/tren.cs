using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tren : MonoBehaviour
{
    public Transform eslemeler, eslesenler;
    public Text kalan;
    public GameObject kapanis;

    public float fkalan = 120f;
    private int dogrular;
    private bool endRun;
    private List<Vector3> esKonumlar;

    public bool timeFlag;

    // Start is called before the first frame update
    void Start()
    {
        endRun = false;
        dogrular = 0;

        List<int> listCreation = new List<int>();
        esKonumlar = new List<Vector3>();
        for (int oa = 1; oa < 21; oa++)
        {
            listCreation.Add(oa);
        }

        int order = 0;
        while (listCreation.Count > 0)
        {
            int randomed = Random.Range(0, listCreation.Count);
            eslemeler.GetChild(order).GetChild(0).GetComponent<Text>().text = listCreation[randomed].ToString();
            esKonumlar.Add(eslemeler.GetChild(order).GetComponent<RectTransform>().position);
            eslemeler.GetChild(order).GetComponent<objeler>().myOrder = order;
            listCreation.RemoveAt(randomed);
            order++;
        }
    }

    void Update()
    {
        if (endRun) return;

        if (fkalan < 0)
        {
            kapanis.SetActive(true);
            kalan.color = Color.red;
            endRun = true;
            StartCoroutine(findTrue());
            return;
        }
        if(!timeFlag)
        {
            fkalan -= Time.deltaTime;
            kalan.text = (fkalan / 60).ToString("0") + ":" + (fkalan % 60).ToString("00");
        }
    }

    private IEnumerator findTrue()
    {
        for (int lea = 0; lea < eslemeler.childCount; lea++)
        {
            int degeri = int.Parse(eslemeler.GetChild(lea).GetChild(0).GetComponent<Text>().text);
            while (eslemeler.GetChild(lea).GetComponent<objeler>().enabled)
            {
                eslemeler.GetChild(lea).GetComponent<RectTransform>().position = Vector3.Lerp(eslemeler.GetChild(lea).GetComponent<RectTransform>().position, eslesenler.GetChild(degeri - 1).GetComponent<RectTransform>().position, 0.2f);
                if (Vector2.Distance(eslemeler.GetChild(lea).GetComponent<RectTransform>().position, eslesenler.GetChild(degeri - 1).GetComponent<RectTransform>().position) < 5)
                {                
                    eslemeler.GetChild(lea).GetComponent<objeler>().deactivate();
                    eslesenler.GetChild(degeri - 1).GetChild(0).GetComponent<Text>().color = Color.green;
                }
                yield return new WaitForSeconds(0.09f);
            }
        }
    }

    public void dogruAlan(int order)
    {
        eslesenler.GetChild(order).GetChild(0).GetComponent<Text>().color = Color.green;
        dogrular++;
        if (dogrular >= 20)
        {
            kalan.color = Color.green;
            endRun = true;
        }
    }

    public void yanlisAlan(int order)
    {
        eslemeler.GetChild(order).GetComponent<RectTransform>().position = esKonumlar[order];
    }
}