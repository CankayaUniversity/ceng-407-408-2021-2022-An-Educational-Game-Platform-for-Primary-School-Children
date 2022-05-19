﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScriptsecondclassforsecondgamepart2 : MonoBehaviour
{
    [SerializeField] Canvas infoCanvas;
    [SerializeField] Transform infoPanel;
    [SerializeField] private float delay = 2f;
    [SerializeField] public saat2 timer;
    void Start()
    {
        infoCanvas.enabled = true;
        infoPanel.gameObject.SetActive(false);
    }

    public void GuestionButton()
    {


        infoPanel.gameObject.SetActive(true);
        timer.isStop = true;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        infoPanel.gameObject.SetActive(false);
        timer.isStop = false;
    }
}
