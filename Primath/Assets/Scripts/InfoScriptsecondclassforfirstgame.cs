using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScriptsecondclassforfirstgame : MonoBehaviour
{
    [SerializeField] Canvas infoCanvas;
    [SerializeField] Transform infoPanel;
   [SerializeField] private float delay = 2f;
    [SerializeField] public Timer2 timer;
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
    
    }

    public void CloseButton()
    {

        infoPanel.gameObject.SetActive(false);
        timer.isStop = false;
    }
}
