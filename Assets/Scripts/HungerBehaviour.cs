using UnityEngine;

public class HungerBehaviour : MonoBehaviour
{
    private Vector3 hunger = new Vector3(0.5f, 0.5f, 0.5f);
    private float hungerDecay = 0.0005f;
    private float hungerEatingBoost = 0.5f;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            var material = collision.gameObject.GetComponent<Renderer>().material;

            hunger += new Vector3(material.color.r * hungerEatingBoost,
                                  material.color.g * hungerEatingBoost,
                                  material.color.b * hungerEatingBoost);
            Destroy(collision.gameObject);
        }
    }

    void FixedUpdate()
    {
        hunger.x = System.Math.Max(hunger.x - hungerDecay, 0);
        hunger.y = System.Math.Max(hunger.y - hungerDecay, 0);
        hunger.z = System.Math.Max(hunger.z - hungerDecay, 0);
        
        material.color = new Color(hunger.x, hunger.y, hunger.z);

        if (hunger.x + hunger.y + hunger.z < 0.00001f)
        {
            //print(hunger.x + ":" + hunger.y + ":" + hunger.z);
            Destroy(this.gameObject);
        }
    }


}