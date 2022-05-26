using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class angry_math : MonoBehaviour
{
    public GameObject panel_s, panel_e, panel_w;
    public Text oranText, stage;
    public Transform taslar, balonlar;
    public sapan SPN;

    public SpriteRenderer soruResmi;
    public GameObject balon, taslarPre;
    public Sprite[] sorular;

    public bool aktif;

    private List<qst> QUESTS;
    private List<string> ANSWERS;
    private Dictionary<int, List<qst>> turQuest;
    private List<string> soruCevaplar;
    private int order_tur, order_soru, order_spawn, dogru, denemeler;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        QUESTS = new List<qst>();
        ANSWERS = new List<string>();
        aktif = false;

        ANSWERS.Add("1/2");
        ANSWERS.Add("1/3");
        ANSWERS.Add("1/4");
        ANSWERS.Add("1/5");
        ANSWERS.Add("2/6");
        ANSWERS.Add("2/3");
        ANSWERS.Add("3/5");
        ANSWERS.Add("4/6");
        ANSWERS.Add("2/5");
        ANSWERS.Add("4/5");
        ANSWERS.Add("3/6");
        ANSWERS.Add("1/6");
        ANSWERS.Add("3/4");

        QUESTS.Add(new qst { correct = "1/2", question = sorular[0] });
        QUESTS.Add(new qst { correct = "2/3", question = sorular[1] });
        QUESTS.Add(new qst { correct = "1/3", question = sorular[2] });
        QUESTS.Add(new qst { correct = "1/4", question = sorular[3] });
        QUESTS.Add(new qst { correct = "3/4", question = sorular[4] });
        QUESTS.Add(new qst { correct = "3/5", question = sorular[5] });
        QUESTS.Add(new qst { correct = "4/5", question = sorular[6] });
        QUESTS.Add(new qst { correct = "4/6", question = sorular[7] });
        QUESTS.Add(new qst { correct = "2/6", question = sorular[8] });

        // Tur oluþturma
        turQuest = new Dictionary<int, List<qst>>();
        for (int kie = 0; kie < 4; kie++)
        {
            List<qst> randomed = new List<qst>();
            while (randomed.Count < 5)
            {
                randomed.Add(QUESTS[Random.Range(0, QUESTS.Count)]);
            }
            turQuest.Add(kie, randomed);
        }
    }

    public void startGame()
    {
        aktif = true;
        panel_s.SetActive(false);
        order_tur = -1;
        dogru = 0;
        denemeler = 0;

        nextTur();
        SPN.CreateRock();
    }

    private void loseGame()
    {
        StopAllCoroutines();
        aktif = false;
        panel_e.SetActive(true);
    }

    public bool pullRock()
    {
        if (taslar.childCount <= 0)
        {
            Invoke("loseGame", 2);
            return false;
        }

        denemeler += 1;
        Destroy(taslar.GetChild(taslar.childCount - 1).gameObject);
        return true;
    }

    private void nextTur()
    {
        order_soru = -1;
        order_tur += 1;

        if (order_tur >= 4)
        {
            StopAllCoroutines();
            aktif = false;
            float oranti = denemeler / dogru;
            oranText.text = "Toplam denemeler: " + denemeler + "\nDoðru sayýsý: " + dogru + "\nOranýnýz: " + System.Math.Round(oranti, 2);
            panel_w.SetActive(true);
            return;
        }

        Vector3 oldy = taslar.position;
        Destroy(taslar.gameObject);
        taslar = Instantiate(taslarPre, oldy, Quaternion.identity).transform;

        askQuestion();
    }

    private void askQuestion()
    {
        order_soru += 1;

        StopAllCoroutines();
        order_spawn = 0;
        for (int ai = 0; ai < balonlar.childCount; ai++)
        {
            Destroy(balonlar.GetChild(ai).gameObject);
        }

        if (order_soru >= 5) nextTur();
        else
        {
            stage.text = "Tur: " + (order_tur + 1) + "/4, Bölüm: " + (order_soru + 1) + "/5";
            soruResmi.sprite = turQuest[order_tur][order_soru].question;

            soruCevaplar = new List<string>();
            soruCevaplar.Add(turQuest[order_tur][order_soru].correct);
            while (soruCevaplar.Count < 6)
            {
                int kieOrder = Random.Range(0, ANSWERS.Count);
                if (!soruCevaplar.Contains(ANSWERS[kieOrder])) soruCevaplar.Add(ANSWERS[kieOrder]);
            }
            Shuffle(soruCevaplar, new System.Random());

            StartCoroutine(balonSpawner());
        }
    }

    public void ansQuestion(string answer)
    {
        if (turQuest[order_tur][order_soru].correct.Equals(answer))
        {
            dogru++;
            askQuestion();
        }
    }

    private class qst
    {
        public string correct { get; set; }
        public Sprite question { get; set; }
    }

    private IEnumerator balonSpawner()
    {
        while (aktif)
        {
            GameObject spawned = Instantiate(balon, balonlar.position, Quaternion.identity);
            spawned.transform.SetParent(balonlar);
            spawned.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = soruCevaplar[order_spawn];
            order_spawn += 1;
            if (order_spawn >= soruCevaplar.Count) order_spawn = 0;

            StartCoroutine(balonMoves(spawned.transform));
            Destroy(spawned, 20);

            yield return new WaitForSeconds(3.5f);
        }
    }

    private IEnumerator balonMoves(Transform poster)
    {
        while (poster != null)
        {
            poster.position += Vector3.up * Time.deltaTime * 2f;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void Shuffle<T>(IList<T> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
