using UnityEngine;

public class SpawnFruitBehavior : MonoBehaviour
{
    public GameObject FruitPrefab;

    private float spawnDelay = 4;
    private float spawnDelayVariance = 2;
    private int spawnForce = 2000;
    private int torqueForce = 10;
    private Color color;

    void Start()
    {
        float r = 0.2f, g = 0.2f, b = 0.2f;

        switch (Random.Range(0, 3))
        {
            case 1:
                r = 0.8f;
                break;
            case 2:
                g = 0.8f;
                break;
            default:
                b = 0.8f;
                break;
        }
        color = new Color(r, g, b);

        var material = GetComponent<Renderer>().material;
        material.color = color;

        var trueSpawnDelay = Random.Range(spawnDelay - spawnDelayVariance, spawnDelay + spawnDelayVariance);
        InvokeRepeating("SpawnFruit", trueSpawnDelay / 2, trueSpawnDelay);
    }
    
    void SpawnFruit()
    {
        var food = Instantiate(FruitPrefab, 
            new Vector3(this.transform.position.x, 1, this.transform.position.z), 
            Quaternion.identity);
        
        var insideUnitSphere = Random.insideUnitSphere;
        var direction = new Vector3(insideUnitSphere.x, Mathf.Abs(insideUnitSphere.y), insideUnitSphere.z);
        
        var foodRb = food.GetComponent<Rigidbody>();
        foodRb.AddForce(direction * spawnForce);

        var normal = Vector3.Cross(this.transform.up, direction);
        foodRb.AddTorque(normal * torqueForce, ForceMode.Impulse);

        var material = food.GetComponent<Renderer>().material;
        material.color = color;
    }
}
