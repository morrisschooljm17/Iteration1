using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{

    [SerializeField] private Transform otherTimeMachine;
    public bool inPast;

    private Vector3 otherTimeMachinePosistion;
    private Vector3 secondPos;

    private void Start()
    {
        otherTimeMachinePosistion = otherTimeMachine.position + new Vector3(0, 0, 0);

    }
    public void timeTravel(Transform player, Transform camera)
    {
        if (inPast)
        {
            player.position = otherTimeMachinePosistion;
            camera.position += new Vector3(50, 0, 0);
            
        }
        else
        {
            player.position = otherTimeMachinePosistion;
            camera.position += new Vector3(-50, 0, 0);
        }

    }
/*    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger");
        objectMove = col.GetComponent<Transform>();
        lever = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Trigger left");
        objectMove = null;
        lever = false;
    }*/
}
