using UnityEngine;

public class SpawnFruitBehavior : MonoBehaviour
{
    public GameObject FruitPrefab;

    private float spawnDelay = 4;
    private float spawnDelayVariance = 2;
    private int spawnForce = 2000;
    private int torqueForce = 10;

    void Start()
    {
        var trueSpawnDelay = Random.Range(spawnDelay - spawnDelayVariance, spawnDelay + spawnDelayVariance);
        InvokeRepeating("SpawnFruit", trueSpawnDelay, trueSpawnDelay);
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
    }
}
