using UnityEngine;

public class CamContoller : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Vector2 limit;
    [SerializeField] float scrollSpeed = 20f;
    [SerializeField] float minY = 20f;
    [SerializeField] float maxY = 150f;
    [SerializeField] float rotationSpeed = 5f;

    void Update()
    {
        Vector3 currLoc = transform.position;

        if (Input.GetKey("w"))
        {
            currLoc.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            currLoc.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            currLoc.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            currLoc.z += speed * Time.deltaTime;
        }
        if (Input.GetMouseButton(2))
        {
            transform.eulerAngles += rotationSpeed * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currLoc.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        currLoc.x = Mathf.Clamp(currLoc.x, -limit.x, limit.x);
        currLoc.y = Mathf.Clamp(currLoc.y, minY, maxY);
        currLoc.z = Mathf.Clamp(currLoc.z, -limit.y, limit.y);


        transform.position = currLoc;
    }
}
