using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTurret : MonoBehaviour
{
    public Transform player;
    public bool m_IsPlayerInRange;
    public Turret turret;
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
            turret.playerLost();
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

                    turret.playerSeen(player);
                }

            }
        }
    }
}
