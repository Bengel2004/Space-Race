using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RocketScript : MonoBehaviour
{
    [Range(0f, 10f)]
    public float force;
    Rigidbody rb;

    float explosionDetermination;
    public float chanceOfExplosion = 70;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        explosionDetermination = Random.Range(0, 100f);
        if (chanceOfExplosion < explosionDetermination)
            explosion();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.up * force);
    }

    void explosion()
    {
        // hier negatieve effecten toevoegen als hij toch ontploft
    }
}
