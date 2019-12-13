using UnityEngine;

public class SocialBehaviour : MonoBehaviour
{
    private float fightThreshold = 1f;
    private Material material;
    private float timeBetweenSpawn = 4;
    private float timeSinceLastSpawn = 0;
    private float timeBeforeMaturity = 5;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timeBeforeMaturity -= Time.deltaTime;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Beast"))
        {
            var color = material.color;
            var colliderMaterial = collision.gameObject.GetComponent<Renderer>().material;
            var colliderColor = colliderMaterial.color;

            var distance = color.r - colliderColor.r + color.g - colliderColor.g + color.b - colliderColor.b;

            if (System.Math.Abs(distance) > fightThreshold)
            {
                print("death - distance: " + distance);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                if (timeSinceLastSpawn > timeBetweenSpawn && timeBeforeMaturity < 0)
                {
                    timeSinceLastSpawn = 0;
                    print("birth");

                    var position = (this.transform.position + collision.transform.position) / 2;
                    var childColor = new Color((color.r + colliderColor.r) / 2,
                        (color.g + colliderColor.g) / 2,
                        (color.b + colliderColor.b) / 2);

                    var child = Instantiate(this.gameObject, position, Quaternion.identity);
                    var childMaterial = child.GetComponent<Renderer>().material;
                    childMaterial.color = childColor;


                }
            }
        }
    }
}
