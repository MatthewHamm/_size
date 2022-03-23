using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    Rigidbody m_Rigidbody;
    bool isGrounded=false;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();    
    }
    void Update()
    {
        if (m_Rigidbody.velocity.magnitude < 0.5 && isGrounded)
        {
            GameObject.Destroy(gameObject,1);
        }
        GameObject.Destroy(gameObject,10);

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.takeDamage();
            GameObject.Destroy(gameObject);
        } else if (collision.gameObject.tag == "Turret") {
            Turret turret = collision.gameObject.GetComponent<Turret>();

            turret.takeDamage();
            GameObject.Destroy(gameObject);
        }else if (collision.gameObject.tag == "Ranged")
        {
            Ranged ranged = collision.gameObject.GetComponent<Ranged>();
            ranged.takeDamage();
            GameObject.Destroy(gameObject);
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

