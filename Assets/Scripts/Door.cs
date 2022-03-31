using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float xAxis;
    [SerializeField] private float yAxis;
    [SerializeField] private float speed;
    [SerializeField] private bool startOpen;
    private Vector3 newPos;
    private Vector3 oldPos;
    private void Start()
    {
        if (startOpen)
        {
            oldPos = transform.position + new Vector3(xAxis, yAxis, 0);
            newPos = transform.position;
        }
        else
        {
            newPos = transform.position + new Vector3(xAxis, yAxis, 0);
            oldPos = transform.position;
        }

    }

    public void openDoor()
    {
        StartCoroutine(SmoothTranslation(newPos, speed));
        
    }

    public void closeDoor()
    {
        StartCoroutine(SmoothTranslation(oldPos, speed));
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

    }
}
