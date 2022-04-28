using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachineDoorOpener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SmoothDoor[] elevators;
    public void startElevator()
    {
        for(int i = 0; i < elevators.Length; i++)
        {
            elevators[i].openDoor();
        }
    }
    int count = 1;
    void OnTriggerEnter2D(Collider2D col){
        if(count ==1){
            startElevator();
            count = 0;
        }
    }
}
