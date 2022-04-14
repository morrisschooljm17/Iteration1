using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        failPrompt.SetActive(true);
        nextLevelDoor.SetActive(false);
    }
}
