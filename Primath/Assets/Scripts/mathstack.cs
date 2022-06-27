using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mathstack : MonoBehaviour
{
    public GameObject yardimEkran, anaEkran, oyunEkrani, oyunKazanma, oyunKaybetme;
    public Text SKT, SOR;
    public GameObject[] OBJ;

    private int score, TY, ANS;
    private bool gonline, helpin;
    private List<islem> islemler;

    [SerializeField] Sound sound;

    void Start()
    {
        islemler = new List<islem>();
        islemler.Add(new islem { answer = 1, bolme = new string[] { "1/1", "3/3", "4/4" }, carpma = new string[] { "1x1" }, cikarma = new string[] { "2-1", "4-3", "1-0" }, toplama = new string[] { "1+0", "0+1" } });
        islemler.Add(new islem { answer = 2, bolme = new string[] { "46/23", "96/48", "36/18" }, carpma = new string[] { "2x1", "1x2" }, cikarma = new string[] { "4-2", "6-4", "5-3" }, toplama = new string[] { "1+1", "2+0" } });
        islemler.Add(new islem { answer = 3, bolme = new string[] { "27/3", "63/21", "12/4" }, carpma = new string[] { "3x1", "1x3" }, cikarma = new string[] { "6-3", "5-2", "4-1" }, toplama = new string[] { "0+3", "2+1", "1+2" } });
        islemler.Add(new islem { answer = 4, bolme = new string[] { "12/3", "48/12", "28/7" }, carpma = new string[] { "2x2", "4x1" }, cikarma = new string[] { "22-18", "30-26", "7-3" }, toplama = new string[] { "2+2", "3+1", "4+0" } });
        islemler.Add(new islem { answer = 5, bolme = new string[] { "10/2", "100/20", "15/3" }, carpma = new string[] { "5x1", "1x5" }, cikarma = new string[] { "7-2", "6-1", "8-3" }, toplama = new string[] { "2+3", "4+1", "5+0" } });
        islemler.Add(new islem { answer = 6, bolme = new string[] { "48/8", "54/9", "18/3" }, carpma = new string[] { "6x1", "2x3", "3x2" }, cikarma = new string[] { "7-1", "6-0", "12-6" }, toplama = new string[] { "2+3", "4+2", "5+1" } });
        islemler.Add(new islem { answer = 7, bolme = new string[] { "49/7", "21/3", "56/8" }, carpma = new string[] { "7x1", "1x7" }, cikarma = new string[] { "14-7", "8-1", "10-3" }, toplama = new string[] { "6+1", "4+3", "2+5" } });
        islemler.Add(new islem { answer = 8, bolme = new string[] { "64/8", "72/9", "24/3" }, carpma = new string[] { "4x2", "8x1" }, cikarma = new string[] { "10-2", "8-0", "9-1" }, toplama = new string[] { "6+2", "4+4", "3+5" } });
        islemler.Add(new islem { answer = 9, bolme = new string[] { "90/10", "81/9", "54/6" }, carpma = new string[] { "9x1", "3x3" }, cikarma = new string[] { "11-2", "19-10", "14-5" }, toplama = new string[] { "7+2", "4+5", "3+6" } });
        islemler.Add(new islem { answer = 10, bolme = new string[] { "80/8", "100/10", "40/4" }, carpma = new string[] { "5x2", "10x1" }, cikarma = new string[] { "18-8", "20-10", "15-5" }, toplama = new string[] { "8+2", "4+6", "5+5" } });
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();

        //islemler.Add(new islem { answer = , bolme = new string[] { }, carpma = new string[] { }, cikarma = new string[] { }, toplama = new string[] { } });
    }

    private IEnumerator testerEngine()
    {
        while (score < 100 && gonline)
        {
            if (!helpin)
            {
                GameObject inent = Instantiate(OBJ[Random.Range(0, OBJ.Length)], new Vector3(0, 120, 0), Quaternion.identity);
                inent.transform.SetParent(oyunEkrani.transform, false);
                inent.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-380, 381), 380, 0);

                //inent.GetComponent<mathstackcubes>().MS = this;
                int ranged = Random.Range(1, islemler.Count + 1);
                inent.transform.GetChild(0).GetComponent<Text>().text = ranged.ToString();
                inent.GetComponent<Button>().onClick.AddListener(() => { answerRT(inent, ranged); });
                yield return new WaitForSeconds(2.5f);
            }
            else yield return new WaitForSeconds(0.25f);
        }
    }

    public void HelperBox()
    {
        yardimEkran.SetActive(!yardimEkran.activeSelf);
        helpin = !helpin;
    }

    public void edgeCrash()
    {
        for (int clen = 0; clen < oyunEkrani.transform.childCount; clen++)
        {
            Destroy(oyunEkrani.transform.GetChild(clen).gameObject);
        }

        score = 0;
        askQuestion();
    }

    public void startRoute(int type)
    {
        TY = type;
        score = 0;
        gonline = true;
        helpin = false;

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
            Debug.LogWarning("OYUNDA HATA\nTY = " + TY + ", aralýk 0 ile 3 arasýnda olmalýdýr.");
        }
    }

    public void answerRT(GameObject ielf, int value)
    {
        if (value == ANS)
        {
            score += 5;
            SKT.text = "Skor: " + score + "/100";
            sound.PlayTrueSound();
            if (score >= 100)
            {
                oyunKazanma.SetActive(true);
                return;
            }
            Destroy(ielf);
        }
        else
        {
            sound.PlayFalseSound();
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