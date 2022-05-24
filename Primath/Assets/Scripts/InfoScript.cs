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
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        ChangePanelActivity(false);
        if (timer != null)
            timer.isStop = false;
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
