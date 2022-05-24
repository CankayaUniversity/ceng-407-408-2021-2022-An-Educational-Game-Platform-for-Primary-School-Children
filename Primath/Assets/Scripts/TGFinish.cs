using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TGFinish : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] public int Score;
    [SerializeField] public int TimeSpent;
    [SerializeField] public int Wrongs;
    private void Awake()
    {
        panel = this.transform.Find("Panel").gameObject;
        panel.SetActive(false);
    }

    public void Rety()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Finished()
    {
        panel.SetActive(true);
        string min = (TimeSpent / 60).ToString("00");
        string sec = (TimeSpent % 60).ToString("00");
        panel.transform.Find("Time").gameObject.GetComponent<Text>().text = min + ":" + sec;

        panel.transform.Find("Wrongs").gameObject.GetComponent<Text>().text = Wrongs.ToString();

        panel.transform.Find("Score").gameObject.GetComponent<Text>().text = "%" + Score.ToString();
    }
}
