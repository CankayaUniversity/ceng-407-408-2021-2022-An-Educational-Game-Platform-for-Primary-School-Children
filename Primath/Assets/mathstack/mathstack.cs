using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mathstack : MonoBehaviour
{
    public GameObject anaEkran, oyunEkrani, oyunKazanma, oyunKaybetme;
    public Text SKT, SOR;
    public GameObject[] OBJ;

    private int score, TY, ANS;
    private bool gonline;
    private List<islem> islemler;

    void Start()
    {
        islemler = new List<islem>();
        islemler.Add(new islem { answer = 1, bolme = new string[] { "1/1", "3/3", "4/4" }, carpma = new string[] { "1x1" }, cikarma = new string[] { "2-1", "4-3", "1-0" }, toplama = new string[] { "1+0", "0+1" } });
        islemler.Add(new islem { answer = 2, bolme = new string[] { "4/2", "6/3", "8/4" }, carpma = new string[] { "2x1", "1x2" }, cikarma = new string[] { "4-2", "6-4", "5-3" }, toplama = new string[] { "1+1", "2+0" } });
        islemler.Add(new islem { answer = 3, bolme = new string[] { "6/2", "9/3", "12/4" }, carpma = new string[] { "3x1", "1x3" }, cikarma = new string[] { "6-3", "5-2", "4-1" }, toplama = new string[] { "0+3", "2+1", "1+2" } });
        islemler.Add(new islem { answer = 4, bolme = new string[] { "12/3", "8/2", "4/1" }, carpma = new string[] { "2x2", "4x1" }, cikarma = new string[] { "6-2", "5-1", "7-3" }, toplama = new string[] { "2+2", "3+1", "4+0" } });
        islemler.Add(new islem { answer = 5, bolme = new string[] { "10/2", "5/1", "15/3" }, carpma = new string[] { "5x1", "1x5" }, cikarma = new string[] { "7-2", "6-1", "8-3" }, toplama = new string[] { "2+3", "4+1", "5+0" } });
        islemler.Add(new islem { answer = 6, bolme = new string[] { "12/2", "6/1", "18/3" }, carpma = new string[] { "6x1", "2x3", "3x2" }, cikarma = new string[] { "7-1", "6-0", "12-6" }, toplama = new string[] { "2+3", "4+2", "5+1" } });
        islemler.Add(new islem { answer = 7, bolme = new string[] { "14/2", "21/3", "7/1" }, carpma = new string[] { "7x1", "1x7" }, cikarma = new string[] { "14-7", "8-1", "10-3" }, toplama = new string[] { "6+1", "4+3", "2+5" } });
        islemler.Add(new islem { answer = 8, bolme = new string[] { "16/2", "8/1", "24/3" }, carpma = new string[] { "4x2", "8x1" }, cikarma = new string[] { "10-2", "8-0", "9-1" }, toplama = new string[] { "6+2", "4+4", "3+5" } });

        //islemler.Add(new islem { answer = , bolme = new string[] { }, carpma = new string[] { }, cikarma = new string[] { }, toplama = new string[] { } });
    }

    private IEnumerator testerEngine()
    {
        while (score < 100 && gonline)
        {
            GameObject inent = Instantiate(OBJ[Random.Range(0, OBJ.Length)], new Vector3(0, 120, 0), Quaternion.identity);
            inent.transform.SetParent(oyunEkrani.transform, false);
            inent.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-400, 401), 400, 0);

            int ranged = Random.Range(1, islemler.Count + 1);
            inent.transform.GetChild(0).GetComponent<Text>().text = ranged.ToString();
            inent.GetComponent<Button>().onClick.AddListener(() => { answerRT(inent, ranged); });
            yield return new WaitForSeconds(2.5f);
        }
    }

    public void startRoute(int type)
    {
        TY = type;
        score = 0;
        gonline = true;

        askQuestion();
        anaEkran.SetActive(false);
        StartCoroutine(testerEngine());
        oyunEkrani.SetActive(true);
    }

    private void askQuestion()
    {
        islem randomed = islemler[Random.Range(0, islemler.Count)];
        ANS = randomed.answer;
        if (TY == 0) SOR.text = randomed.toplama[Random.Range(0, randomed.toplama.Length)];
        else if (TY == 1) SOR.text = randomed.cikarma[Random.Range(0, randomed.cikarma.Length)];
        else if (TY == 2) SOR.text = randomed.carpma[Random.Range(0, randomed.carpma.Length)];
        else if (TY == 3) SOR.text = randomed.bolme[Random.Range(0, randomed.bolme.Length)];
        else
        {
            gonline = false;
            SOR.text = "OYUN HATASI";
            Debug.LogWarning("OYUNDA HATA\nTY = " + TY + ", aral�k 0 ile 3 aras�nda olmal�d�r.");
        }
    }

    public void answerRT(GameObject ielf, int value)
    {
        if (value == ANS)
        {
            score += 5;
            SKT.text = score + "/100";
            if (score >= 100)
            {
                oyunKazanma.SetActive(true);
                return;
            }
            Destroy(ielf);
        }
        else
        {
            ielf.GetComponent<Button>().enabled = false;
            ielf.GetComponent<Image>().color = Color.black;
            ielf.transform.GetChild(0).gameObject.SetActive(false);
            ielf.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        askQuestion();
    }

    private class islem
    {
        public int answer { get; set; }
        public string[] toplama { get; set; }
        public string[] cikarma { get; set; }
        public string[] bolme { get; set; }
        public string[] carpma { get; set; }
    }
}