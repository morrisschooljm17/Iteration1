using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverandShut : MonoBehaviour
{
    [SerializeField] private Door[] doorsToOpen;
    [SerializeField] private Door[] doorsToClose;
    private AudioSource audioSource;

public void activate()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        for (int i = 0; i < doorsToOpen.Length; i++)
        {
            doorsToOpen[i].openDoor();
        }
        for (int i = 0; i < doorsToClose.Length; i++)
        {
            doorsToClose[i].closeDoor();
        }

    }


}

