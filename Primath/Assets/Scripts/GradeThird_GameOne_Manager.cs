using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class GradeThird_GameOne_Manager : MonoBehaviour
{
    public Numbers numbers;

    private int min ,max, answer, left, right;
    private int[] answers = new int[3];
    private int score, qNumber;
    public int maxQuestionNum = 30;

    private char sign;

    string txt;

    Timer timer;

    GameObject question;
    GameObject ansPanel;
    GameObject mainCanvas;
    GameObject qNum, ansNum;
    GameObject redGirl, wolf;
    List<GameObject> buttons = new List<GameObject>();

    [SerializeField] Sound sound;

    public TGFinish tgFinish;

    void Start()
    {
        score = 0;
        qNumber = 0;
        min = numbers.min;
        max = numbers.max;
        sign = numbers.sign;

        mainCanvas = GameObject.Find("MainCanvas");
        ansPanel = mainCanvas.transform.Find("AnswerPanel").gameObject;
        question = mainCanvas.transform.Find("GameQuestion").transform.Find("Question").gameObject;
        redGirl = mainCanvas.transform.Find("RedGirl").gameObject;
        wolf = mainCanvas.transform.Find("Wolf").gameObject;
        qNum = mainCanvas.transform.Find("QuNum").gameObject;
        ansNum = mainCanvas.transform.Find("Ans").gameObject;
        timer = mainCanvas.transform.Find("Timer").gameObject.GetComponent<Timer>();

        buttons.Add(ansPanel.transform.Find("Answer_1").gameObject);
        buttons.Add(ansPanel.transform.Find("Answer_2").gameObject);
        buttons.Add(ansPanel.transform.Find("Answer_3").gameObject);

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();

        ansNum.GetComponent<Text>().text = "<color=green>" + score.ToString() + "</color>";

        PrintQuestion();
    }

    private void Update()
    {
        if(!timer.isTimeExposed() || qNumber >= maxQuestionNum)
        {
            Finish();
        }
    }
    private void GanerateQuestion(Text text)
    {
        left = Random.Range(min, max);
        right = Random.Range(min, max);
        txt = left.ToString() + " " + sign + " " + right.ToString();
        text.text = txt;

        qNumber++;
        qNum.GetComponent<Text>().text = qNumber.ToString() + "/" + maxQuestionNum.ToString();

        switch (sign)
        {
            case '+': answer = left + right; break;
            case '-': answer = left - right; break;
            case '*': answer = left * right; break;
            case '/': answer = left / right; break;
        }


        answers[0] = answer - 1;
        answers[1] = answer;
        answers[2] = answer + 1;
        MakeAnswers();
    }

    private void MakeAnswers()
    {
        Shuffle(answers);

        buttons[0].transform.Find("Text").GetComponent<Text>().text = (answers[0]).ToString();
        buttons[1].transform.Find("Text").GetComponent<Text>().text = (answers[1]).ToString();
        buttons[2].transform.Find("Text").GetComponent<Text>().text = (answers[2]).ToString();
    }

    private void Shuffle(int[] answers)
    {
        int index = Random.Range(0, answers.Length);
        int temp = Random.Range(0, answers.Length);
        
        while(temp == index)
            temp = Random.Range(0, answers.Length);

        answers[index] += answers[temp];
        answers[temp] = answers[index] - answers[temp];
        answers[index] -= answers[temp];
    }

    private void PrintQuestion()
    {
        GanerateQuestion(question.GetComponent<Text>());
    }
    
    public void CheckAnswer(Text text)
    {
        if (text.text == answer.ToString())
        {
            score++;
            ansNum.GetComponent<Text>().text = "<color=green>" + score.ToString() + "</color>";
            text.GetComponentInParent<Animator>().Play("Correct");
            sound.PlayTrueSound();
            redGirl.GetComponent<MoveImage>().Move();
        }
        else
        {
            text.GetComponentInParent<Animator>().Play("False");
            sound.PlayFalseSound();
            wolf.GetComponent<MoveImage>().Move();
        }

        StartCoroutine(WaitForAnim(text.GetComponentInParent<Button>()));

        PrintQuestion();
    }

    IEnumerator WaitForAnim(Button button)
    {
        yield return new WaitForSeconds(1);

        button.interactable = false;
        button.interactable = true;
    }

    public void Finish()
    {
        timer.isStop = true;
        int pastTime = timer.time - timer.CountTime;
        int wrongs = qNumber - score - 1;

        tgFinish.TimeSpent = pastTime;
        tgFinish.Wrongs = wrongs;

        if (pastTime < 40)
            pastTime = 0;
        else
            pastTime -= 40;

        tgFinish.Score = (score * 6) - (pastTime / 2) - (wrongs * 2) + 10;
        tgFinish.Finished();
    }
}