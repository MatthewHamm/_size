using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Restart : MonoBehaviour
{
    public Button restart;
    void Start() {
        restart.onClick.AddListener(onClick);
    }
    // Start is called before the first frame update
    void onClick()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
    }
}
