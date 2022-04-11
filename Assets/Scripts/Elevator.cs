using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private bool up;

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
        doorOpen =! doorOpen;
    }
    /*IEnumerator TimedDoorShut(float time)
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
            transform.position = Vector3.Lerp(transform.position, target, t / speed);
            yield return null;
        }

    }*/

}
