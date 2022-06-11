using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GT_GameTwo_Manager : MonoBehaviour
{
    public Numbers numbers;

    private int min, max, left, right;

    private const string EQUAL = "EqualBucket", SMALLER = "SmallerBucket", BIGGER = "BiggerBucket";

    [SerializeField] Sound sound;

    Transform corePanelTrans, topPanelTrans;
    Text leftTxt, rightTxt;
    Text qeustTxt, scoreTxt;
    int score, qNumber;

    void Start()
    {
        corePanelTrans = GameObject.Find("MainCanvas").transform.Find("GameCorePanel").transform;
        leftTxt = corePanelTrans.Find("QuestionLeftImage").transform.Find("QuestionLeftText").gameObject.GetComponent<Text>();
        rightTxt = corePanelTrans.Find("QuestionRightImage").transform.Find("QuestionRightText").gameObject.GetComponent<Text>();

        topPanelTrans = GameObject.Find("MainCanvas").transform.Find("TopPanel").transform;
        qeustTxt = topPanelTrans.Find("QuestionNumber").gameObject.GetComponent<Text>();
        scoreTxt = topPanelTrans.Find("Score").gameObject.GetComponent<Text>();

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();

        score = 0;
        qNumber = 0;
        min = numbers.min;
        max = numbers.max;
        PrintQuestion();
    }
    private void PrintQuestion()
    {
        qNumber++;
        left = Random.Range(min, max);
        right = Random.Range(min, max);

        leftTxt.text = left.ToString();
        rightTxt.text = right.ToString();

        scoreTxt.text = score.ToString();
        qeustTxt.text = qNumber.ToString();
    }

    public void chekForEquation(string name)
    {
        if (left == right && name == EQUAL)
        {
            score++;
            sound.PlayTrueSound();
        }
        else if (left < right && name == SMALLER)
        {
            score++;
            sound.PlayTrueSound();
        }
        else if (left > right && name == BIGGER)
        {
            score++;
            sound.PlayTrueSound();
        }
        else
        {
            sound.PlayFalseSound();
        }
        StartCoroutine(WaitForSec(name));
    }

    IEnumerator WaitForSec(string name)
    {
        yield return new WaitForSeconds(1);

        this.GetComponent<BucketGame>().TurnOrginalPos(name);
        PrintQuestion();
    }
}
