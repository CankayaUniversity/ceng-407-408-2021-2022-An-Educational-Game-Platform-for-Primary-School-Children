using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mathmatch : MonoBehaviour
{
    public Transform[] PNL;
    public GameObject panel_h, panel_w;
    public Text timeT, infoT, puanT;

    private Dictionary<int, List<card>> CARDS;
    private List<Vector3> konumlar;

    private int order, donner, PUAN;
    private float zaman;
    private bool aktif;

    void Start()
    {
        Application.targetFrameRate = 60;
        aktif = false;

        CARDS = new Dictionary<int, List<card>>();
        CARDS.Add(0, new List<card>());
        CARDS[0].Add(new card { kie = 1, questions = new string[] { "1+0", "0+1", "2-1", "1-0", "1x1", "1x1", "2/2", "1/1" } });
        CARDS[0].Add(new card { kie = 2, questions = new string[] { "1+1", "2+0", "2-0", "3-1", "1x2", "2x1", "4/2", "6/3" } });
        CARDS[0].Add(new card { kie = 3, questions = new string[] { "1+2", "3+0", "4-1", "3-0", "3x1", "1x3", "6/2", "3/1" } });
        CARDS.Add(1, new List<card>());
        CARDS[1].Add(new card { kie = 5, questions = new string[] { "2+3", "1+4", "7-3", "6-1", "5x1", "1x5", "10/2", "5/1" } });
        CARDS[1].Add(new card { kie = 6, questions = new string[] { "3+3", "4+2", "8-2", "10-4", "6x1", "2x3", "12/2", "6/1" } });
        CARDS[1].Add(new card { kie = 8, questions = new string[] { "4+4", "3+5", "9-1", "10-2", "4x2", "1x8", "16/2", "8/1" } });
        CARDS.Add(2, new List<card>());
        CARDS[2].Add(new card { kie = 10, questions = new string[] { "6+4", "8+2", "14-4", "13-3", "5x2", "2x5", "10/1", "100/10" } });
        CARDS[2].Add(new card { kie = 13, questions = new string[] { "8+5", "6+7", "15-2", "19-6", "13x1", "1x13", "26/2", "13/1" } });
        CARDS[2].Add(new card { kie = 14, questions = new string[] { "9+5", "8+6", "19-5", "17-3", "2x7", "14x1", "28/2", "14/1" } });
        CARDS.Add(3, new List<card>());
        CARDS[3].Add(new card { kie = 16, questions = new string[] { "8+8", "12+6", "20-4", "26-10", "8x2", "4x4", "32/2", "48/3" } });
        CARDS[3].Add(new card { kie = 18, questions = new string[] { "15+3", "11+7", "26-8", "20-2", "60x3", "9x2", "36/2", "54/3" } });
        CARDS[3].Add(new card { kie = 19, questions = new string[] { "10+9", "17+2", "25-6", "22-3", "1x19", "19x1", "38/2", "57/3" } });
        CARDS.Add(4, new List<card>());
        CARDS[4].Add(new card { kie = 21, questions = new string[] { "15+6", "7+14", "32-11", "46-25", "7x3", "3x7", "105/5", "126/6" } });
        CARDS[4].Add(new card { kie = 28, questions = new string[] { "18+8", "14+12", "35-9", "43-17", "2x14", "4x6", "84/3", "112/4" } });
        CARDS[4].Add(new card { kie = 31, questions = new string[] { "18+13", "24+7", "50-19", "45-14", "1x31", "31x1", "93/3", "155/5" } });
        CARDS.Add(5, new List<card>());
        CARDS[5].Add(new card { kie = 48, questions = new string[] { "34+14", "26+22", "86-38", "71-23", "8x6", "4x12", "280/5", "384/8" } });
        CARDS[5].Add(new card { kie = 54, questions = new string[] { "35+19", "49+5", "68-14", "95-41", "6x9", "3x18", "324/6", "162/3" } });
        CARDS[5].Add(new card { kie = 63, questions = new string[] { "49+14", "20+43", "90-27", "112-49", "3x21", "9x7", "252/4", "315/5" } });

        konumlar = new List<Vector3>();
        for (int hb = 0; hb < PNL[0].childCount; hb++)
        {
            konumlar.Add(PNL[0].GetChild(hb).position);
        }

        startGame();
    }

    void Update()
    {
        if (!aktif) return;

        zaman += Time.deltaTime;
        timeT.text = Mathf.RoundToInt(zaman).ToString();
    }

    public void iDone()
    {
        donner += 1;

        if (donner >= PNL[0].childCount)
        {
            donner = 0;
            nexTour();
        }
    }

    public void startGame()
    {
        zaman = 0;
        donner = 0;
        order = 2;
        aktif = true;

        nexTour();
    }

    public void HelpBox()
    {
        aktif = !aktif;
        panel_h.SetActive(!panel_h.activeSelf);
    }

    public void defPosition(int pos)
    { PNL[0].GetChild(pos).position = konumlar[pos]; }

    private void nexTour()
    {
        order += 1;

        if (order != 0) PUAN += Mathf.Max(0, Mathf.RoundToInt(160 - zaman));
        zaman = 0;

        infoT.text = "Tur: " + (order - 2) + "/" + (CARDS.Count - 3);

        if (order >= CARDS.Count)
        {
            aktif = false;
            panel_w.SetActive(true);
            puanT.text = "Puanýnýz: " + PUAN;

            return;
        }

        List<card> dass = CARDS[order];
        List<Transform> transed = new List<Transform>();
        for (int hb = 0; hb < PNL[1].childCount; hb++)
        {
            transed.Add(PNL[1].GetChild(hb));
        }

        for (int hb = 0; hb < PNL[0].childCount; hb++)
        {
            PNL[0].GetChild(hb).GetComponent<matches>().resetMC();
            PNL[1].GetChild(hb).GetComponent<matches>().resetMC();

            card cr = dass[Random.Range(0, dass.Count)];

            PNL[0].GetChild(hb).GetComponent<matches>().KEY = cr.kie;
            int ranged = Random.Range(0, transed.Count);
            transed[ranged].GetComponent<matches>().KEY = cr.kie;

            PNL[0].GetChild(hb).GetChild(0).GetComponent<Text>().text = cr.questions[Random.Range(0, cr.questions.Length)];
            transed[ranged].GetChild(0).GetComponent<Text>().text = cr.questions[Random.Range(0, cr.questions.Length)];

            transed.RemoveAt(ranged);
        }
    }

    private class card
    {
        public string[] questions { get; set; }
        public int kie { get; set; }
    }
}