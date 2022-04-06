using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElevatorController : MonoBehaviour
{
    //[SerializeField] private Transform elevato;
    [SerializeField] private Elevator elevator;
    [SerializeField] private float xAxis;
    [SerializeField] private float yAxis;
    [SerializeField] private float speed;
    [SerializeField] private bool timedDoor;
    [SerializeField] private float doorCloseTimer;
    bool doorOpen = false;
    private Vector3 newPos;
    private Vector3 oldPos;
    private void Start()
    {
        newPos = elevator.transform.position + new Vector3(xAxis, yAxis, 0);
        oldPos = elevator.transform.position;
    }
    public void openDoor()
    {
        if (elevator.getUp())
        {
            elevator.setUp();
            StopAllCoroutines();
            StartCoroutine(SmoothTranslation(oldPos, speed));
        }
        else
        {
            elevator.setUp();
            StopAllCoroutines();
            StartCoroutine(SmoothTranslation(newPos, speed));
            if (timedDoor)
            {
                StartCoroutine(TimedDoorShut(doorCloseTimer));
            }
        }
        doorOpen = !doorOpen;
    }
    IEnumerator TimedDoorShut(float time)
    {
        yield return new WaitForSeconds(time);
        openDoor();

    }
    IEnumerator SmoothTranslation(Vector3 target, float speed)
    {
        float t = 0.0f;

        while (t < speed)
        {
            t += Time.deltaTime;
            elevator.transform.position = Vector3.Lerp(elevator.transform.position, target, t / speed);
            yield return null;
        }

    }


}
