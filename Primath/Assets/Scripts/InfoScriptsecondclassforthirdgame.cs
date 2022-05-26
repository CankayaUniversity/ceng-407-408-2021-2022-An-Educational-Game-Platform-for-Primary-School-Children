using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScriptsecondclassforthirdgame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Canvas infoCanvas;
    [SerializeField] Transform infoPanel;
    [SerializeField] private float delay = 2f;
  
    void Start()
    {
        infoCanvas.enabled = true;
        infoPanel.gameObject.SetActive(false);
    }

    public void GuestionButton()
    {


        infoPanel.gameObject.SetActive(true);
       
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        infoPanel.gameObject.SetActive(false);
       
    }
}
