using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saat2 : MonoBehaviour
{
    public InputField input;
    public InputField input2;
    public float kalan;
    public Text TXT;
    public int istenilensaat;
    public int istenilendakika;
    public int number;
    public int number2;
    public GameObject kazanmaPanel;
    public GameObject kaybetmepanali;
    private bool flag = true;
    public bool isStop = false;
    [SerializeField] Canvas infoCanvas;
    [SerializeField] Transform infoPanel;
    [SerializeField] private float delay = 2f;

    [SerializeField] Sound sound;

    void Start()
    {
        infoCanvas.enabled = true;
        infoPanel.gameObject.SetActive(false);
        TXT.text = "KALAN SANİYE: " + kalan;

        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<Sound>();
    }

    void Update()
    {
        if (isStop == false)
        {
            if (kalan != 0 && flag == true)
                kalan -= Time.deltaTime;
        }
       

        TXT.text = "KALAN SANİYE: " + Mathf.RoundToInt(kalan);
        if (kalan == 0 || kalan < 0)
        {
            kaybetmepanali.SetActive(true);
            sound.PlayFalseSound();
            flag = false;
        }
    }
    public void GetInputSaat()
    {
        int.TryParse(input.text, out int result);
        number = result;
        Debug.Log("saat:" + number);
    }
    public void GetInputDakika()
    {
        int.TryParse(input2.text, out int result2);
        number2 = result2;

        Debug.Log("dakika:" + number2);
        Results();

    }
    public void Results()
    {
        if (number == istenilensaat && number2 == istenilendakika && kalan > 0)
        {
            kazanmaPanel.SetActive(true);
            sound.PlayTrueSound();
            flag = false;
        }
       
        else
        {
            infoCanvas.gameObject.SetActive(true);
            infoPanel.gameObject.SetActive(true);
            sound.PlayFalseSound();
            StartCoroutine(Delay());
             Debug.Log("Tekrar Dene");
        }
           
    }
     IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        infoPanel.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
   
    // Update is called once per frame
   
}
