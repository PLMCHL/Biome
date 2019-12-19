using UnityEngine;

public class ControlBehavior : MonoBehaviour
{
    Vector3 scrollDirection = new Vector3(0, -1, 1);
    float sensitivity = 10f;
    public GameObject beastPrefab;

    void Update()
    {
        ExecuteMove();
        ExecuteZoom();
        ExecuteClickSpawn();
    }

    void ExecuteMove()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        this.transform.position += new Vector3(xAxisValue, 0.0f, zAxisValue);
    }

    void ExecuteZoom()
    {
        this.transform.position += (scrollDirection * Input.GetAxis("Mouse ScrollWheel") * sensitivity);
    }

    void ExecuteClickSpawn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.name == "Ground")
                {
                    Instantiate(beastPrefab, hit.point + Vector3.up, Quaternion.identity);
                }
            }
        }
    }
}
