using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dondurma : MonoBehaviour
{
    public Transform[] kulahlar;
    public Transform topListesi;
    public Text sure;
    public GameObject kapanis, bitis;

    [SerializeField] Sound sound;

    private Dictionary<int, string[]> math;
    private Dictionary<int, kulah> yerler;
    private List<Vector3> konumlar;

    private bool endRun;
    private int dogru;
    [SerializeField] private float time = 90f;
    public bool timeFlag;

    void Start()
    {
        endRun = false;
        dogru = 0;
        timeFlag = false;

        math = new Dictionary<int, string[]>();
        math.Add(0, new string[4] { "3-3", "4-4", "0+0", "0+0" });
        math.Add(1, new string[4] { "1-0", "2-1", "1+0", "1+0" });
        math.Add(2, new string[4] { "3-1", "4-2", "1+1", "0+2" });
        math.Add(3, new string[4] { "3-0", "4-1", "2+1", "3+0" });
        math.Add(4, new string[4] { "5-1", "4-0", "3+1", "2+2" });
        math.Add(5, new string[4] { "6-1", "7-2", "2+3", "4+1" });
        math.Add(6, new string[4] { "7-1", "9-3", "3+3", "4+2" });
        math.Add(7, new string[4] { "7-0", "8-1", "2+5", "3+4" });
        math.Add(8, new string[4] { "9-1", "8-0", "3+5", "6+2" });
        math.Add(9, new string[4] { "9-0", "11-2", "6+3", "4+5" });

        // þimdi random 4 tane key alma
        List<int> placedINT = new List<int>();
        for (int kl = 0; kl < 4; kl++)
        {// bir dk düþüneyim
            int randomKEY = keyBul(placedINT);
            kulahlar[kl].name = randomKEY.ToString();
            kulahlar[kl].GetChild(0).GetComponent<Text>().text = randomKEY.ToString();
            placedINT.Add(randomKEY);
        }

        yerler = new Dictionary<int, kulah>();
        yerler.Add(int.Parse(kulahlar[0].name), new kulah { dL = yerBul(int.Parse(kulahlar[0].name)), dex = 0 });
        yerler.Add(int.Parse(kulahlar[1].name), new kulah { dL = yerBul(int.Parse(kulahlar[1].name)), dex = 0 });
        yerler.Add(int.Parse(kulahlar[2].name), new kulah { dL = yerBul(int.Parse(kulahlar[2].name)), dex = 0 });
        yerler.Add(int.Parse(kulahlar[3].name), new kulah { dL = yerBul(int.Parse(kulahlar[3].name)), dex = 0 });

        konumlar = new List<Vector3>();
        for (int gb = 0; gb < topListesi.childCount; gb++)
        {
            int randomKey = placedINT[Random.Range(0, placedINT.Count)];
            string isl;
            if (gb < 4) isl = math[randomKey][Random.Range(0, 2)];
            else isl = math[randomKey][Random.Range(2, 4)];
            topListesi.GetChild(gb).GetChild(0).GetComponent<Text>().text = isl;
            topListesi.GetChild(gb).GetComponent<toplar>().order = gb;
            topListesi.GetChild(gb).GetComponent<toplar>().KEY = randomKey;
            konumlar.Add(topListesi.GetChild(gb).GetComponent<RectTransform>().position);
        }

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();
    }

    void Update()
    {
        if (endRun) return;

        if (time < 0)
        {
            kapanis.SetActive(true);
            sure.color = Color.red;
            endRun = true;
            StartCoroutine(otodiz());
            return;
        }
        if(!timeFlag)
            ProcessTime();
    }

    private void ProcessTime()
    {
        time -= Time.deltaTime;
        string minutes = (time / 60).ToString("0");
        string seconds = (time % 60).ToString("00");
        sure.text = "Süre : " + minutes + ":" + seconds;
    }

    private IEnumerator otodiz()
    {
        for (int lea = 0; lea < topListesi.childCount; lea++)
        {
            int degeri = topListesi.GetChild(lea).GetComponent<toplar>().KEY;
            while (topListesi.GetChild(lea).GetComponent<toplar>().enabled)
            {
                topListesi.GetChild(lea).GetComponent<RectTransform>().position = Vector3.Lerp(topListesi.GetChild(lea).GetComponent<RectTransform>().position, yerBul(degeri, true), 0.2f);
                if (Vector2.Distance(topListesi.GetChild(lea).GetComponent<RectTransform>().position, yerBul(degeri, true)) < 5)
                {
                    yerler[degeri].dex += 1;
                    topListesi.GetChild(lea).GetComponent<toplar>().deactivate();
                }
                yield return new WaitForSeconds(0.09f);
            }
        }
        kapanis.SetActive(false);
        bitis.SetActive(true);
    }

    public void dogruAlan(int order, int key)
    {
        topListesi.GetChild(order).GetComponent<RectTransform>().position = yerBul(key, true);
        yerler[key].dex += 1;

        dogru++;
        if (dogru >= topListesi.childCount)
        {
            endRun = true;
            sure.color = Color.yellow;
            bitis.SetActive(true);
        }
        sound.PlayTrueSound();
    }

    public void yanlisAlan(int order)
    {
        topListesi.GetChild(order).GetComponent<RectTransform>().position = konumlar[order];
        sound.PlayFalseSound();
    }

    private Vector3 yerBul(int key, bool belirginkonum = false)
    {
        if (belirginkonum) return yerler[key].dL + new Vector3(0, 50 * yerler[key].dex, 0);
        else return GameObject.Find(key.ToString()).GetComponent<RectTransform>().position + new Vector3(0, 80, 0);
    }

    private int keyBul(List<int> plc)
    {
        int randomKEY = Random.Range(1, 10);
        if (plc.Contains(randomKEY)) return keyBul(plc);
        else return randomKEY;
    }

    private class kulah
    {
        public Vector3 dL { get; set; }
        public int dex { get; set; }
    }
}