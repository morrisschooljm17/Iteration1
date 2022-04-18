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
    private int numberOfScenes = 8;
    // Start is called before the first frame update
    void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lever && sceneNumber < numberOfScenes)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else if(Input.GetKeyDown(KeyCode.E) && lever && sceneNumber == numberOfScenes)
        {
            SceneManager.LoadScene(0);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger");
        lever = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Trigger left");
        lever = false;
    }
}
