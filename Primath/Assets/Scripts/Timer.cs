using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int CountTime;
    string minutes, seconds;

    void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (CountTime > 0)
        {
            UpdateTime();

            yield return new WaitForSeconds(1);

            CountTime--;
        }
    }

    private void UpdateTime()
    {
        minutes = (CountTime / 60).ToString("00");
        seconds = (CountTime % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }
}
