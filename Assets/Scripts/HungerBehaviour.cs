using UnityEngine;

public class HungerBehaviour : MonoBehaviour
{
    private Vector3 hunger = new Vector3();
    private float hungerDecay = 0.0005f;
    private float hungerEatingBoost = 0.5f;

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
            hunger += new Vector3(0, hungerEatingBoost, 0);
            Destroy(collision.gameObject);
        }
    }

    void FixedUpdate()
    {
        hunger -= new Vector3(0, hungerDecay, 0);
        material.color = originalColor + new Color(hunger.x, hunger.y, hunger.z);
    }


}