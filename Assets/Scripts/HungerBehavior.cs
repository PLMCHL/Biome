using System;
using UnityEngine;

public class HungerBehavior : MonoBehaviour
{
    private Vector3 hunger = new Vector3(0.5f, 0.5f, 0.5f);
    private float hungerDecay = 0.001f;
    private float hungerColorBoost = 0.25f;
    private float hungerSizeBoost = 0.25f;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            UpdateColor(collision);
            UpdateSize();
            Destroy(collision.gameObject);
        }
    }

    void UpdateColor(Collision collision)
    {
        var colliderMaterial = collision.gameObject.GetComponent<Renderer>().material;

        hunger += new Vector3(colliderMaterial.color.r * hungerColorBoost,
                              colliderMaterial.color.g * hungerColorBoost,
                              colliderMaterial.color.b * hungerColorBoost);
    }

    void UpdateSize()
    {
        this.transform.localScale += new Vector3(hungerSizeBoost, hungerSizeBoost, hungerSizeBoost);
    }

    void FixedUpdate()
    {
        hunger.x = System.Math.Max(hunger.x - hungerDecay, 0);
        hunger.y = System.Math.Max(hunger.y - hungerDecay, 0);
        hunger.z = System.Math.Max(hunger.z - hungerDecay, 0);
        
        material.color = new Color(hunger.x, hunger.y, hunger.z);

        if (hunger.x + hunger.y + hunger.z < 0.00001f)
        {
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("Dead", true);
            Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}