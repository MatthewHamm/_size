using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    Rigidbody m_Rigidbody;
    bool isGrounded=false;
    ParticleSystem poof;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        poof = GetComponentInChildren<ParticleSystem>();
        poof.Pause();
    }
    void Update()
    {
        if (m_Rigidbody.velocity.magnitude < 0.5 && isGrounded)
        {
            if (!poof.isPlaying)
            {
                poof.Play();
                print(poof);
                Destroy(gameObject, 1);
            }
         

        }
        else
        {
            Destroy(gameObject, 10);
        }


    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.takeDamage();
            Destroy(gameObject,0.1f);
        } else if (collision.gameObject.tag == "Turret") {
            Turret turret = collision.gameObject.GetComponent<Turret>();

            turret.takeDamage();
            Destroy(gameObject, 0.1f);
        }
        else if (collision.gameObject.tag == "Ranged")
        {

            Ranged ranged = collision.gameObject.GetComponent<Ranged>();
            ranged.takeDamage();
            Destroy(gameObject, 0.1f);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Wall")
        {
            isGrounded = true;

        }
    }
}

