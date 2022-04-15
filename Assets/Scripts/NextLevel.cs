using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool lever;
    private String nextLevelName;
    // Start is called before the first frame update
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        string sceneNumber = new string(scene.Where(Char.IsDigit).ToArray());
        int result = Int32.Parse(sceneNumber)+1;
        nextLevelName = "level" + result;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lever && nextLevelName == "level8")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(KeyCode.E) && lever){
            SceneManager.LoadScene(nextLevelName);
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
