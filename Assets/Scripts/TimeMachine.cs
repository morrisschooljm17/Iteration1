using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{

    [SerializeField] private Transform otherTimeMachine;
    public bool inPast;

    private Vector3 otherTimeMachinePosistion;

    Vector3 cameraPosNew;
    Vector3 local;

    private void Start()
    {
        otherTimeMachinePosistion = otherTimeMachine.position + new Vector3(0, 0, 0);

    }
    public void timeTravel(Rigidbody2D player, Camera camera)
    {

        if (inPast)
        {
            cameraPosNew = camera.transform.position + new Vector3(50, 0, 0);
        }
        else
        {
            cameraPosNew = camera.transform.position + new Vector3(-50, 0, 0);
        }
        local = player.transform.localScale;
        StartCoroutine(SpinPlayer(player, camera));
    }

    IEnumerator SpinPlayer(Rigidbody2D player, Camera camera)
    {
        Vector3 position = player.transform.position;

        for (int i = 0; i < 100; i++)
        {
            if (i < 49)
            {
                player.transform.localScale += new Vector3(-.1f, -.1f, 0);
                player.transform.position = position;
            }
            else
            {
                player.transform.localScale += new Vector3(.1f, .1f, 0);
                player.transform.position = otherTimeMachinePosistion;
                camera.transform.position = cameraPosNew;
            }

            player.transform.Rotate(Vector3.forward * -45);
            yield return new WaitForSeconds(.01f);
        }
        player.transform.localScale = local;
        player.transform.rotation = Quaternion.identity;
        player.transform.position = otherTimeMachinePosistion;
        yield return null;
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
