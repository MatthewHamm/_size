using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCount : MonoBehaviour
{
    public Text levels;
    // Start is called before the first frame update
    void Start()
    {
        levels.text = (SceneManager.GetActiveScene().buildIndex).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
