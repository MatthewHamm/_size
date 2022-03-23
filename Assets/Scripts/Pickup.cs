using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
       

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            Player player = other.GetComponent<Player>();
            source = player.GetComponent<AudioSource>();
            source.Play();
            player.health += 20;
            GameObject.Destroy(gameObject);

        }
    }
    // Update is called once per frame

}
