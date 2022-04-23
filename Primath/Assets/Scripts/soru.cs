using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soru : MonoBehaviour
{
    public Text[] TXT;
    public GameObject[] OBJ;
    public Image[] BTI;
    public Sprite[] BIT;

    private List<question> questions;
    private int order, dogru;
    private float time;
    private bool inquest;

    // Start is called before the first frame update
    void Start()
    {
        inquest = false;
        order = -1;
        dogru = 0;
        time = 30;

        questions = new List<question>();
        questions.Add(new question { qst = "4+2", correct = 0, options = new string[2] { "6", "2" } });
        questions.Add(new question { qst = "2-2", correct = 1, options = new string[2] { "4", "0" } });
        questions.Add(new question { qst = "0+5", correct = 0, options = new string[2] { "5", "3" } });
        questions.Add(new question { qst = "3-2", correct = 1, options = new string[2] { "2", "1" } });
        questions.Add(new question { qst = "6+2", correct = 1, options = new string[2] { "4", "8" } });
        questions.Add(new question { qst = "8-4", correct = 0, options = new string[2] { "4", "3" } });
        questions.Add(new question { qst = "3+5", correct = 0, options = new string[2] { "8", "6" } });
        questions.Add(new question { qst = "4+3", correct = 1, options = new string[2] { "5", "7" } });
        questions.Add(new question { qst = "7-4", correct = 0, options = new string[2] { "3", "2" } });

        while (questions.Count > 5)
        {
            questions.RemoveAt(Random.Range(0, questions.Count));
        }

        askQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if (OBJ[1].activeSelf || inquest) return;

        if (time < 0)
        {
            inquest = true;
            StartCoroutine(animBIT());
            return;
        }
        time -= Time.deltaTime;
        TXT[1].text = Mathf.RoundToInt(time).ToString();
    }

    private void askQuestion()
    {
        order++;
        TXT[0].text = dogru + " / 5";

        if (order > 4)
        {
            OBJ[0].SetActive(false);
            OBJ[1].SetActive(true);
            return;
        }

        TXT[2].text = questions[order].qst;
        TXT[3].text = questions[order].options[0];
        TXT[4].text = questions[order].options[1];
        if (questions[order].correct == 0)
        {
            BTI[0].sprite = BIT[0];
            BTI[1].sprite = BIT[1];
        }
        else
        {
            BTI[0].sprite = BIT[1];
            BTI[1].sprite = BIT[0];
        }

        time = 30;
    }

    public void answer(int los)
    {
        if (inquest) return;

        if (los == questions[order].correct) dogru++;
        inquest = true;
        StartCoroutine(animBIT());
    }

    private class question
    {
        public string qst { get; set; }
        public string[] options { get; set; }
        public int correct { get; set; }
    }

    private IEnumerator animBIT()
    {
        for (int times = 0; times < 2; times++)
        {
            float alpha = 0;
            BTI[0].color = new Color { a = 0, b = 255, g = 255, r = 255 };
            BTI[1].color = new Color { a = 0, b = 255, g = 255, r = 255 };
            while (alpha < 1)
            {
                alpha += 0.05f;
                BTI[0].color = new Color { a = alpha, b = 255, g = 255, r = 255 };
                BTI[1].color = new Color { a = alpha, b = 255, g = 255, r = 255 };
                yield return new WaitForSeconds(0.03f);
            }
            while (alpha > 0)
            {
                alpha -= 0.05f;
                BTI[0].color = new Color { a = alpha, b = 255, g = 255, r = 255 };
                BTI[1].color = new Color { a = alpha, b = 255, g = 255, r = 255 };
                yield return new WaitForSeconds(0.03f);
            }
        }

        inquest = false;
        askQuestion();
    }
}
