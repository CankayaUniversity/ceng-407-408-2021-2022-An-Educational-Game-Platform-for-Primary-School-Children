using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumericCount : MonoBehaviour
{
    public List<Transform> fieldHolders;
    [HideInInspector] public List<Text> fields;
    [Space]
    public List<Transform> cevaplar;
    public List<Text> sıralama;
    [Space]
    public InfoScript info;
    public TGFinish tGFinish;
    public int fieldNum;
    public int countNum = 6;
    public string emtpy = " ";
    private int range;

    private int score;

    public int maxYanlis = 3;
    public Text yanlisTxt;
    private int hataSayisi = 0; // Her yanlış 10 puan

    private int dogruSıralama;

    [Space]
    public float secs = 5f;

    public Transform secondGamePanel;
    private System.Random rng = new System.Random();

    private void Awake()
    {
        score = 100;
        Shuffle<Transform>(fieldHolders);
        secondGamePanel.gameObject.SetActive(false);
        foreach (Transform gm in fieldHolders)
        {
            fields.Add(gm.Find("Text").gameObject.GetComponent<Text>());
        }
        yanlisTxt.text = "<color=red>" + hataSayisi.ToString() + "</color>/" + maxYanlis.ToString(); //+ "/<color=green>" + "</color>"
        dogruSıralama = 1;
    }
    private void Start()
    {
        fieldNum = fieldHolders.Count;
        info.secondStatge = false;
        FillFields();
    }

    private void Update()
    {
        if(dogruSıralama > fields.Count)
        {
            int timeSpent = info.timer.time - info.timer.CountTime;
            tGFinish.TimeSpent = timeSpent;
            tGFinish.Wrongs = hataSayisi;

            if (timeSpent <= 10)
                timeSpent = 0;
            else
                timeSpent -= 10;

            score -= timeSpent / 2;
            if (score < 0)
                score = 0;

            tGFinish.Score = score;
            info.timer.isStop = true;
            tGFinish.Finished();
        }
    }

    public void Shuffle<T>(List<T> list)
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
    private void FillFields()
    {
        range = Random.Range(2, 7);
        
        for(int i = 0; i < range; i++)
        {
            fields[i].text = ((i+1) * countNum).ToString();
        }
        for(int i = range; i < fieldNum; i++)
        {
            fields[i].text = emtpy;
        }

        for(int i = 2; i < range; i++)
        {
            Destroy(cevaplar[0].gameObject);
            cevaplar.RemoveAt(0);
        }

        for(int i = 0; i < cevaplar.Count; i++)
        {
            cevaplar[i].Find("Text").gameObject.GetComponent<Text>().text = ((range + i + 1) * countNum).ToString();
        }
    }

    public void AddToTheLine(Text text)
    {
        if (cevaplar.Count > 0)
            return;

        bool isthere = sıralama.Exists(x => x == text);

        if (isthere)
        {
            int isthereIndex = sıralama.FindIndex(x => x == text);

            if(sıralama[isthereIndex].color == Color.red)
            {
                sıralama[isthereIndex].color = Color.black;
                sıralama.RemoveAt(isthereIndex);
            }
        }
        else
        {
            sıralama.Add(text);
            int index = sıralama.Count - 1;

            if (sıralama[index].text == (dogruSıralama * countNum).ToString())
            {
                sıralama[index].color = Color.green;
                dogruSıralama++;
            }
            else
            {
                sıralama[index].color = Color.red;
                hataSayisi++;
                score -= 10;
                yanlisTxt.text = "<color=red>" + hataSayisi.ToString() + "</color>" + "/<color=green>" + maxYanlis.ToString() + "</color>";
            }
        }
    }

    public void DestroyCevap(GameObject game)
    {
        int index = cevaplar.FindIndex(x => x == game.transform);
        Destroy(cevaplar[index].gameObject);
        cevaplar.RemoveAt(index);

        if(cevaplar.Count <= 0)
        {
            secondGamePanel.gameObject.SetActive(true);
            info.secondStatge = true;
            info.timer.isStop = true;
            StartCoroutine(DelaySec());
        }
    }

    IEnumerator DelaySec()
    {
        yield return new WaitForSeconds(secs);
        secondGamePanel.gameObject.SetActive(false);
        info.timer.isStop = false;
    }
}
