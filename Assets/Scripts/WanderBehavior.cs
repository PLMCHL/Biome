using UnityEngine;

public class WanderBehavior : MonoBehaviour
{
    private Rigidbody rb;

    private float turnAngle = 20f;
    private float speed = 5f;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        var unitInCircle = Random.insideUnitCircle;
        direction = new Vector3(unitInCircle.x, 0, unitInCircle.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Roam();
    }

    void Roam()
    {
        var angle = Random.Range(-turnAngle / 2, turnAngle / 2);
        direction = Quaternion.Euler(0, angle, 0) * direction;
        direction.Normalize();
        
        this.transform.Translate(direction * speed * Time.deltaTime);
    }
}
