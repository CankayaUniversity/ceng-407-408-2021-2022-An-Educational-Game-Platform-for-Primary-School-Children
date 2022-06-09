using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScriptsecondclassforsecondgame : MonoBehaviour
{
    [SerializeField] Canvas infoCanvas;
    [SerializeField] Transform infoPanel;
    [SerializeField] private float delay = 2f;
    [SerializeField] public saat timer;
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
    public void CloseButton()
    {

        infoPanel.gameObject.SetActive(false);
        timer.isStop = false;
    }
}
