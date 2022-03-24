using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradeThird_GameOne_Manager : MonoBehaviour
{
    public Numbers numbers;

    private int min ,max, answer, left, right;
    private int[] answers = new int[3];
    private int score, qNumber;

    private char sign;

    string txt;

    GameObject question;
    GameObject mainCanvas;
    GameObject qNum, ansNum;
    List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        score = 0;
        qNumber = 0;
        min = numbers.min;
        max = numbers.max;
        sign = numbers.sign;

        mainCanvas = GameObject.Find("MainCanvas");
        question = mainCanvas.transform.Find("GameQuestion").transform.Find("Question").gameObject;

        qNum = mainCanvas.transform.Find("QuNum").gameObject;
        ansNum = mainCanvas.transform.Find("Ans").gameObject;

        buttons.Add(mainCanvas.transform.Find("Answer_1").gameObject);
        buttons.Add(mainCanvas.transform.Find("Answer_2").gameObject);
        buttons.Add(mainCanvas.transform.Find("Answer_3").gameObject);

        ansNum.GetComponent<Text>().text = score.ToString();

        PrintQuestion();
    }

    private void GanerateQuestion(Text text)
    {
        left = Random.Range(min, max);
        right = Random.Range(min, max);
        txt = left.ToString() + " " + sign + " " + right.ToString();
        text.text = txt;

        qNumber++;
        qNum.GetComponent<Text>().text = qNumber.ToString();

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

    public void PrintQuestion()
    {
        GanerateQuestion(question.GetComponent<Text>());
    }
    
    public void CheckAnswer(Text text)
    {
        if (text.text == answer.ToString())
        {
            score++;
            ansNum.GetComponent<Text>().text = score.ToString();
        }
        PrintQuestion();
    }
}
