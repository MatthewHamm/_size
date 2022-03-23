using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    public Button quit;
    void Start()
    {
        quit.onClick.AddListener(onClick);
    }
    // Start is called before the first frame update
    void onClick()
    {
        Application.Quit(0);
    }
}
