using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;
    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;
    [SerializeField] private float moveSpeed;
    private bool inPast = true;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Transform cameraMove;
    [SerializeField] private Transform door;
    private Vector3 newPos;
    public LayerMask groundLayer;

    bool isgrounded = true;
    bool onTimeMachine;
    bool onLever;

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
        /*        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    mainRigidbody.AddForce(new Vector2(-moveSpeed, 0));
                    mainSpriteRenderer.flipX = false;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    mainRigidbody.AddForce(new Vector2(moveSpeed, 0));
                    mainSpriteRenderer.flipX = true;
                }*/
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
                inPast = false;
                transform.position += newPos;
                //futurePlayer.position += newPos;
                cameraMove.position += newPos;
            }
            else if (onLever)
            {

                door.position += new Vector3(0, 5, 0);
            }

        }

        Vector2 move = mainRigidbody.velocity;
        float hor = Input.GetAxis("Horizontal");
        if (hor < 0)
        {
            mainSpriteRenderer.flipX = false;
        }
        else if (hor > 0)
        {
            mainSpriteRenderer.flipX = true;
        }

        move.x = hor * moveSpeed;
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            isgrounded = false;
            move.y = jumpSpeed;
        }
        mainRigidbody.velocity = move;

        StartCoroutine(MoveFutureSelf(move));
        IEnumerator MoveFutureSelf(Vector3 move)
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
