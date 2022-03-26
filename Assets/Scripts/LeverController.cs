using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Transform door;
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
        newPos = door.transform.position + new Vector3(xAxis, yAxis, 0);
        oldPos = door.transform.position;
    }
    public void openDoor()
    {
        if (doorOpen)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothTranslation(oldPos, speed));
        }
        else
        {
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
            door.transform.position = Vector3.Lerp(door.transform.position, target, t / speed);
            yield return null;
        }

    }


}
