using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;
    private LeverController leverController;
    private LeverandShut leverAndShutController;
    private ElevatorController elevatorController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool moveFuturePlayer(Vector2 direction, Vector2 move,  bool hitTime, bool hitLever, bool hitLevernadShut,bool elevator, float time)
    {
        StartCoroutine(MoveFutureSelf());
        IEnumerator MoveFutureSelf()
        {
            yield return new WaitForSeconds(time);
            if (direction.x < 0)
            {
                futureSpriteRenderer.flipX = false;
            }
            else if (direction.x > 0)
            {
                futureSpriteRenderer.flipX = true;
            }


            if (hitTime)
            {
                Destroy(transform.gameObject);
            }
            if (hitLever)
            {
                leverController.openDoor();
            }
            if (hitLevernadShut)
            {
                leverAndShutController.activate();
            }
            if (elevator)
            {
                elevatorController.openDoor();
            }
            futureBody.position = move + new Vector2(50, 0);

        }
        return !hitTime;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "lever")
        {
            leverController = col.GetComponent<LeverController>();
        }
        if (col.gameObject.tag == "LeverandShut")
        {
            leverAndShutController = col.GetComponent<LeverandShut>();
        }
        if (col.gameObject.tag == "Elevator")
        {
            elevatorController = col.GetComponent<ElevatorController>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "lever")
        {
            leverController = null;
        }
        if (col.gameObject.tag == "LeverandShut")
        {
            leverAndShutController = null;
        }
        if (col.gameObject.tag == "Elevator")
        {
            elevatorController = null;
        }
    }


}
