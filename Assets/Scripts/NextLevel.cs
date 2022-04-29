using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool lever;
    private int sceneNumber;
    private int numberOfScenes = 0;
    public bool keyExists;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("key") != null)
        {
            keyExists = true;
        }
        else 
        {
            keyExists = false;
        }
        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        bool stop = true;
        while(stop){
            try{
                SceneManager.GetSceneByBuildIndex(numberOfScenes);
                numberOfScenes++;
            }
            catch{
                stop = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !keyExists && lever && sceneNumber < numberOfScenes)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else if(Input.GetKeyDown(KeyCode.E) && !keyExists && lever && sceneNumber == numberOfScenes)
        {
            SceneManager.LoadScene(0);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        lever = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        lever = false;
    }
}
