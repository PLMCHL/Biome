using UnityEngine;

public class EatingBehaviour : MonoBehaviour
{
    public int ingested { get; private set; } = 0;

    private Material material;
    private Color originalColor;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            ingested++;
            material.color = originalColor + new Color(ingested, 0, 0);

            Destroy(collision.gameObject);
        }
    }
}