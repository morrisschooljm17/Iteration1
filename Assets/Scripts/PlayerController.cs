using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;

    [SerializeField] private SpriteRenderer mainSpriteRenderer;

    [SerializeField] private float moveSpeed;
    private bool inPast = true;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Transform cameraMove;
    [SerializeField] private Transform door;
    [SerializeField] private FuturePlayerController futurePlayerController;
    private Vector3 newPos;
    public LayerMask groundLayer;

    bool isgrounded = true;
    bool onTimeMachine;
    bool onLever;
    public bool isThereAFuturePlayer;



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (inPast == true)
        {
            newPos = new Vector3(50, 0, 0);
        }
        else
        {
            newPos = new Vector3(-50, 0, 0);
        }
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
        bool hitTime = false;
        bool hitLever = false;
        
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
                inPast = !inPast;
                transform.position += newPos;
                //futurePlayer.position += newPos;
                cameraMove.position += newPos;
                hitTime = true;
            }
            else if (onLever)
            {
                door.position += new Vector3(0, 5, 0);
                hitLever = true;
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
        if (isThereAFuturePlayer)
        {
            isThereAFuturePlayer = futurePlayerController.moveFuturePlayer(move, hitTime, hitLever);
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
