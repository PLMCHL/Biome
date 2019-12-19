using UnityEngine;

public class WanderBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float turnAngle = 20f;
    private float speed = 5f;
    private Vector3 direction;
    private float detectionRange = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        var unitInCircle = Random.insideUnitCircle;
        direction = new Vector3(unitInCircle.x, 0, unitInCircle.y);
    }

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, detectionRange);
        GameObject target = null;
        float closestDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Food"))
            {
                var distance = (this.transform.position - collider.gameObject.transform.position).magnitude;
                if (distance > closestDistance)
                {
                    continue;
                }

                closestDistance = distance;
                target = collider.gameObject;
            }
        }

        if (target != null)
        {
            // TODO: projection necessary?
            direction = Vector3.ProjectOnPlane(target.transform.position - this.transform.position, Vector3.up);
        }
        else
        {
            var angle = Random.Range(-turnAngle / 2, turnAngle / 2);
            direction = Quaternion.Euler(0, angle, 0) * direction;
        }
        
        direction.Normalize();
        this.transform.position += direction * speed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction);

        Gizmos.color = new Color(1, 1, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, detectionRange);
    }
}
