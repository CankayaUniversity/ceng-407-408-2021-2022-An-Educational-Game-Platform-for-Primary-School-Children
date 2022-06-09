using UnityEngine;

public class mathstackcubes : MonoBehaviour
{
    public mathstack MS;

    private bool triggered = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("large(Clone)") || collision.gameObject.name.Equals("small(Clone)") || collision.gameObject.name.Equals("medium(Clone)")) triggered = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    { if (collision.gameObject.name.Equals("edge") && triggered) MS.edgeCrash(); }

    void OnTriggerStay2D(Collider2D collision)
    { if (collision.gameObject.name.Equals("edge") && triggered) MS.edgeCrash(); }
}