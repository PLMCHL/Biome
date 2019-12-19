using UnityEngine;

public class SocialBehavior : MonoBehaviour
{
    private float fightThreshold = 0.5f;
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

            var distance =  + color.g - colliderColor.g + color.b - colliderColor.b;

            if (System.Math.Abs(color.r - colliderColor.r) < fightThreshold ||
                System.Math.Abs(color.g - colliderColor.g) < fightThreshold ||
                System.Math.Abs(color.b - colliderColor.b) < fightThreshold)
            {
                if (timeSinceLastSpawn > timeBetweenSpawn && timeBeforeMaturity < 0)
                {
                    timeSinceLastSpawn = 0;

                    var position = (this.transform.position + collision.transform.position) / 2;
                    var childColor = new Color((color.r + colliderColor.r) / 2,
                        (color.g + colliderColor.g) / 2,
                        (color.b + colliderColor.b) / 2);

                    var child = Instantiate(this.gameObject, position, Quaternion.identity);
                    var childMaterial = child.GetComponent<Renderer>().material;
                    childMaterial.color = childColor;
                }
            }
            else
            {
                print("death");

                GameObject loser;
                if (this.transform.localScale.x > collision.transform.localScale.x)
                {
                    loser = collision.gameObject;
                }
                else
                {
                    loser = this.gameObject;
                }

                var animator = loser.GetComponent<Animator>();
                animator.SetBool("Dead", true);
                Destroy(loser, animator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }
}
