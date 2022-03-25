using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;

    [SerializeField] private float moveSpeed;
    private bool inPast = true;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private Transform door;
    private Vector3 newPos;
    public LayerMask groundLayer;

    bool isgrounded = true;
    bool onTimeMachine;
    bool onLever;
    bool hitLever = false;
    bool hitTime = false;

    // Start is called before the first frame update
    void Start()
    {
        if (inPast == true)
        {
            newPos = new Vector3(50, 0, 0);
        }
        else
        {
            newPos = new Vector3(-50, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        hitLever = false;
        hitTime = false;

        bool IsGrounded()
        {
            Vector2 position = transform.position;
            Vector2 direction = Vector2.down;
            float distance = 1.215f;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
            Debug.Log(hit.distance);
            if (hit.collider != null)
            {
                return true;
            }

            return false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onTimeMachine)
            {
                hitTime = true;
                //futurePlayer.position += newPos;
            }
            else if (onLever)
            {
                hitLever = true;
            }

        }

        Vector2 move = futureBody.velocity;
        float hor = Input.GetAxis("Horizontal");

        move.x = hor * moveSpeed;
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            isgrounded = false;
            move.y = jumpSpeed;
        }

        StartCoroutine(MoveFutureSelf(move, hitTime, hitLever));
        IEnumerator MoveFutureSelf(Vector3 move, bool hitLever, bool hitTime)
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
            
            if (hitLever)
            {
                door.position += new Vector3(0, 5, 0);
            }
            else if (hitTime)
            {
                Destroy(transform.parent.gameObject);
            }
            futureBody.velocity = move;
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
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
    }


}
