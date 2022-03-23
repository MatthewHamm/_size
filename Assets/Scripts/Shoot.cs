using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject projectile;
    public float shootForce;
    public AudioSource audioSource;
    public Light otherLight;
    bool fired;
    int fireCount;
    // Use this for initialization
    void Start()
    {
        fireCount = 0;
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && !fired)
        {
            fireCount = 0;
            fired = true;
            this.GetComponentInParent<Player>().health -= 5;
            audioSource.Play();
            GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        }else if (Input.GetButton("Fire2") && !otherLight.enabled)
        {
            otherLight.enabled=true;
        }
        else if (Input.GetButton("Fire2") && otherLight.enabled)
        {
            otherLight.enabled = false;
        }
        if (fireCount > 30)
        {
            fired = false;
        }
        fireCount++;
    }
}