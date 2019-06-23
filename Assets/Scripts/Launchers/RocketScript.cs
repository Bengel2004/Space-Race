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

    Animator anim;
    public GameObject Thruster;
    AudioSource Audio;

    bool LaunchRocket = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        explosionDetermination = Random.Range(0, 100f);
        if (chanceOfExplosion < explosionDetermination)
            explosion();

        anim = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
        Thruster.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("RocketSpawn"))
            LaunchRocket = true;

        if (LaunchRocket)
        {
            if (!Audio.isPlaying)
            {
                Audio.Play();
                Thruster.SetActive(true);
            }

        }
    }

    private void FixedUpdate()
    {
        if(LaunchRocket)
            rb.AddForce(Vector3.up * force);
    }

    void explosion()
    {
        // hier negatieve effecten toevoegen als hij toch ontploft
    }
}
