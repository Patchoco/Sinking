using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadLevel1()
    {
        SceneManager.LoadScene("Stage1");
    }

public void quitGame()
    {
        Application.Quit();
    }

}
