using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public Text health;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.text = this.GetComponentInParent<Player>().health.ToString();
    }
}
