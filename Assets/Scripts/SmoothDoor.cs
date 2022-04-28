using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDoor : MonoBehaviour
{
    [SerializeField] private bool up;

    [SerializeField] private float xAxis;
    [SerializeField] private float yAxis;
    [SerializeField] private float speed;
    [SerializeField] private bool timedDoor;
    [SerializeField] private float doorCloseTimer;
    public bool doorOpen = false;
    private Vector3 newPos;
    private Vector3 oldPos;

    private void Start()
    {
        newPos = transform.position + new Vector3(xAxis, yAxis, 0);
        oldPos = transform.position;
    }
    private void Update()
    {
        if (doorOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            //transform.Translate(newPos * speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, oldPos, speed * Time.deltaTime);
            //transform.Translate(oldPos * speed * Time.deltaTime);
        }
    }
    public void openDoor()
    {
        doorOpen = true;
    }
        public void closeDoor()
    {
        doorOpen = false;
    }
}
