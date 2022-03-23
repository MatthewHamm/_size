using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vision : MonoBehaviour
{
    public Transform player;
    public bool m_IsPlayerInRange;
    public Enemy enemy;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            m_IsPlayerInRange = true;
            player = other.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_IsPlayerInRange = false;
            enemy.playerLost();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    print("seen");
                    enemy.playerSeen(player);
                }

            }
        }
    }
}
