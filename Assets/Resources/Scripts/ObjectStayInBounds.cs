using UnityEngine;

public class ObjectStayInBounds : MonoBehaviour
{
    Vector2 CamBounds;
    // Start is called before the first frame update
    void Start()
    {
        CamBounds = Camera.main.ViewportToWorldPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        //Stay in screen bounds
        if (transform.position.x > -CamBounds.x)
            pos.x = -CamBounds.x;
        if (transform.position.x < CamBounds.x)
            pos.x = CamBounds.x;

        if (transform.position.y > -CamBounds.y)
            pos.y = -CamBounds.y;
        if (transform.position.y < CamBounds.y)
            pos.y = CamBounds.y;

        transform.position = (Vector3)pos;
    }
}
