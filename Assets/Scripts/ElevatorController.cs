using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElevatorController : MonoBehaviour
{
    [SerializeField] private Elevator[] elevators;
    public void startElevator()
    {
        for(int i =0; i < elevators.Length; i++)
        {
            elevators[i].openDoor();
        }

    }



}
