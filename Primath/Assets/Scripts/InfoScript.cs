using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    [SerializeField] Canvas infoCanvas;
    [SerializeField] Transform infoPanel;
    [SerializeField] Transform infoPanelSec;
    [SerializeField] private float delay = 2f;
    [SerializeField] public Timer timer;
    public dondurma dondurma;
    public soru soru;
    public tren tren;
    public bool secondStatge;
    void Start()
    {
        if(infoPanelSec != null)
            infoPanelSec.gameObject.SetActive(false);

        infoCanvas.enabled = true;
        infoPanel.gameObject.SetActive(false);
        secondStatge = false;
    }

    public void GuestionButton()
    {
        ChangePanelActivity(true);
        if (timer != null)
            timer.isStop = true;
        if (dondurma != null)
            dondurma.timeFlag = true;
        if (soru != null)
            soru.timeFlag = true;
        if (tren != null)
            tren.timeFlag = true;

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        ChangePanelActivity(false);
        if (timer != null)
            timer.isStop = false;
        if (dondurma != null)
            dondurma.timeFlag = false;
        if (soru != null)
            soru.timeFlag = false;
        if (tren != null)
            tren.timeFlag = false;
    }

    private void ChangePanelActivity(bool sec)
    {
        if (!secondStatge)
            infoPanel.gameObject.SetActive(sec);
        else
        {
            infoPanelSec.gameObject.SetActive(sec);
        }
    }
}
