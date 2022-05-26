using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rock : MonoBehaviour
{
    public angry_math ANM;

    public void Release()
    {
        tas_bulutu.instance.Clear();
        StartCoroutine(CreatePathPoints());
    }

    IEnumerator CreatePathPoints()
    {
        while (ANM.aktif)
        {
            tas_bulutu.instance.CreateCurrentPathPoint(transform.position);
            yield return new WaitForSeconds(tas_bulutu.instance.timeInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string ballonText = collision.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        Destroy(collision.gameObject, 0.5f);

        ANM.ansQuestion(ballonText);
    }
}
