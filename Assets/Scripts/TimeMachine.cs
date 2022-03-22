using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{
    [SerializeField] private Transform playerMove;
    [SerializeField] private Transform cameraMove;
    [SerializeField] private bool inPast;

    private bool lever;
    private Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        if(inPast == true)
        {
            newPos = new Vector3(50, 0, 0);
        }
        else
        {
            newPos = new Vector3(-50, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lever)
        {
            playerMove.position += newPos;
            cameraMove.position += newPos;

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
