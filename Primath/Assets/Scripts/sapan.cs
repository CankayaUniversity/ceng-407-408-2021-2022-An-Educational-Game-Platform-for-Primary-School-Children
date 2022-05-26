using UnityEngine;

public class sapan : MonoBehaviour
{
    public angry_math ANM;

    public LineRenderer[] ipCizgileri;
    public Transform[] ipPozisonlari;
    public Transform merkez, sabitleme;
    public Vector3 currentPosition;
    public GameObject rockPrefab;

    public float maxLength, bottomBoundary, rockPositionOffset;
    public float force;

    private Rigidbody2D rock;
    private Collider2D rockCollider;
    private bool isMouseDown;

    void Start()
    {
        ipCizgileri[0].positionCount = 2;
        ipCizgileri[1].positionCount = 2;
        ipCizgileri[0].SetPosition(0, ipPozisonlari[0].position);
        ipCizgileri[1].SetPosition(0, ipPozisonlari[1].position);
    }

    void Update()
    {
        if (!ANM.aktif) return;

        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = merkez.position + Vector3.ClampMagnitude(currentPosition - merkez.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (rockCollider) rockCollider.enabled = true;
        }
        else ResetStrips();
    }

    public void CreateRock()
    {
        if (!ANM.pullRock()) return;

        GameObject creRock = Instantiate(rockPrefab);
        creRock.GetComponent<rock>().ANM = ANM;
        rock = creRock.GetComponent<Rigidbody2D>();
        rockCollider = rock.GetComponent<Collider2D>();
        rockCollider.enabled = false;

        rock.isKinematic = true;

        ResetStrips();
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
        currentPosition = sabitleme.position;
    }

    private void Shoot()
    {
        rock.isKinematic = false;
        Vector3 rockForce = (currentPosition - merkez.position) * force * -1;
        rock.velocity = rockForce;

        rock.GetComponent<rock>().Release();

        Destroy(rock.gameObject, 8);

        rock = null;
        rockCollider = null;
        Invoke("CreateRock", 2);
    }

    private void ResetStrips()
    {
        currentPosition = sabitleme.position;
        SetStrips(currentPosition);
    }

    private void SetStrips(Vector3 position)
    {
        ipCizgileri[0].SetPosition(1, position);
        ipCizgileri[1].SetPosition(1, position);

        if (rock)
        {
            Vector3 dir = position - merkez.position;
            rock.transform.position = position + dir.normalized * rockPositionOffset;
            rock.transform.right = -dir.normalized;
        }
    }

    private Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}
