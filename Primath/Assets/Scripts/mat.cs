using UnityEngine;
using UnityEngine.UI;

public class mat : MonoBehaviour
{
    public string bitis;
    public string[] yanlislar;
    public GameObject sorular;

    public float hareketArtisi = 0.1f;

    public Text[] TXT;
    public GameObject bitisPanel, kazanmaPanel;

    private int life;

    void Start()
    {
        Application.targetFrameRate = 30;

        life = 3;
        TXT[0].text = "CAN: " + life;
    }

    // Update is called once per frame
    void Update()
    {
        if (bitisPanel.activeSelf || kazanmaPanel.activeSelf) return;

        if (Input.GetKey(KeyCode.UpArrow)) transform.position = transform.position + new Vector3(0, hareketArtisi, 0);
        if (Input.GetKey(KeyCode.LeftArrow)) transform.position = transform.position + new Vector3(-hareketArtisi, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow)) transform.position = transform.position + new Vector3(0, -hareketArtisi, 0);
        if (Input.GetKey(KeyCode.RightArrow)) transform.position = transform.position + new Vector3(hareketArtisi, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(bitis))
        {
            kazanmaPanel.SetActive(true);
            collision.enabled = false;
            return;
        }

        foreach (string yanlis in yanlislar)
        {
            if (collision.name.Equals(yanlis))
            {
                life -= 1;
                if (life == 0) bitisPanel.SetActive(true);

                TXT[0].text = "CAN: " + life;
                break;
            }
        }

        Destroy(sorular.transform.GetChild(0).gameObject);
        Destroy(sorular.transform.GetChild(1).gameObject);
    }
}
