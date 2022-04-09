using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time;
    private int coinCount;

    public TMP_Text text;

    void Start()
    {
        coinCount = 0;
        text.text = "" + (int)time;
    }

    
    void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
            text.text = "" + (int)time;
        }

        coinCount = PlayerPrefs.GetInt("1tl")+ PlayerPrefs.GetInt("50krs")+ PlayerPrefs.GetInt("25krs") + PlayerPrefs.GetInt("10krs") + PlayerPrefs.GetInt("5krs") + PlayerPrefs.GetInt("1krs");
        if (time >= 0 && coinCount == 6)
        {
            text.text = "Kazandın!!!";
        }
        else if(time <= 0 && coinCount != 6)
        {
            text.text = "Kaybettin!!!";
        }
    }
}
