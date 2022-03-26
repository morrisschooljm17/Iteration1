using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;

    [SerializeField] private Transform door;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool moveFuturePlayer(Vector2 move, bool hitTime, bool hitLever)
    {
        StartCoroutine(MoveFutureSelf(move, hitTime, hitLever));
        IEnumerator MoveFutureSelf(Vector3 move, bool hitTime, bool hitLever)
        {
            yield return new WaitForSeconds(5f);
            if (move.x < 0)
            {
                futureSpriteRenderer.flipX = false;
            }
            else if (move.x > 0)
            {
                futureSpriteRenderer.flipX = true;
            }
            futureBody.velocity = move;

            if (hitTime)
            {
                Destroy(transform.gameObject);
            }
            if (hitLever)
            {
                door.position += new Vector3(0, 5, 0);
            }
            
        }
        return !hitTime;
    }
/*    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TimeMachine")
        {
            Debug.Log("Time enter");
            onTimeMachine = true;
        }
        if (col.gameObject.tag == "lever")
        {
            onLever = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "TimeMachine")
        {
            Debug.Log("Time leave");
            onTimeMachine = false;
        }
        if (col.gameObject.tag == "lever")
        {
            onLever = false;
        }
    }*/


}
