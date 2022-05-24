using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int time;
    public int CountTime;
    private string minutes, seconds;
    public bool isStop = false;

    void Start()
    {
        time = CountTime;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (CountTime > 0)
        {
            if (!isStop)
            {
                UpdateTime();

                yield return new WaitForSeconds(1);

                CountTime--;
            }
            yield return null;
        }
    }

    private void UpdateTime()
    {
        minutes = (CountTime / 60).ToString("0");
        seconds = (CountTime % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }

    public int getTimeAsSec()
    {
        return CountTime;
    }

    public void setTimeAsSec(int time)
    {
        CountTime = time;
    }

    public bool isTimeExposed()
    {
        return CountTime > 0;
    }
}
