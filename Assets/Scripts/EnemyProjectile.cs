using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        print("fired");
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject.Destroy(gameObject, 10);
    }
    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.tag);

        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.health -= 20;
            GameObject.Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Turret"|| collision.gameObject.tag == "Ranged") {

        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
