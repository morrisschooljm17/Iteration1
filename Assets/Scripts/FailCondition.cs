using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailCondition : MonoBehaviour
{


    [SerializeField] private GameObject failPrompt;
    [SerializeField] private GameObject nextLevelDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
