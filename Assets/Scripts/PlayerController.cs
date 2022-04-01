using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Transform cameraMove;
    [SerializeField] private FuturePlayerController futurePlayerController;
    [SerializeField] private float futurePlayerDelay;
    private TimeMachine timeMachine;
    private LeverController leverController;
    private LeverandShut leverAndShut;
    public LayerMask groundLayer;

    bool onTimeMachine;
    bool onLever;
    bool resetMachine;
    bool onLeverandShut;
    public bool isThereAFuturePlayer;



    // Start is called before the first frame update
    void Start()
    {


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
        bool hitTime = false;
        bool hitLever = false;
        bool hitLeverandShut = false;

        bool IsGrounded()
        {
            /*            Vector2 position = transform.position;
                        Vector2 direction = Vector2.down;
                        float distance = 1.215f;

                        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
                        Debug.Log(hit.distance);
                        if (hit.collider != null)
                        {
                            return true;
                        }

                        return false;*/
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0, 0, 0), 0f, Vector2.down, .01f, groundLayer);
            return raycastHit2d.collider != null;

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onTimeMachine)
            {
                timeMachine.timeTravel(transform, cameraMove);
                hitTime = true;
            }
            else if (onLever)
            {
                leverController.openDoor();
                hitLever = true;
            }
            else if (resetMachine)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (onLeverandShut)
            {
                leverAndShut.activate();
                hitLeverandShut = true;
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
            move.y = jumpSpeed;

        }
        mainRigidbody.velocity = move;
        
        if (isThereAFuturePlayer)
        {
            isThereAFuturePlayer = futurePlayerController.moveFuturePlayer(move, transform.position, hitTime, hitLever, hitLeverandShut, futurePlayerDelay);
        }
        

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TimeMachine")
        {
            timeMachine = col.GetComponent<TimeMachine>();
            onTimeMachine = true;
        }
        if (col.gameObject.tag == "lever")
        {
            leverController = col.GetComponent<LeverController>();
            onLever = true;
        }
        if (col.gameObject.tag == "ResetMachine")
        {
            resetMachine = true;
        }
        if (col.gameObject.tag == "LeverandShut")
        {
            leverAndShut = col.GetComponent<LeverandShut>();
            onLeverandShut = true;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "TimeMachine")
        {
            timeMachine = null;
            onTimeMachine = false;
        }
        if (col.gameObject.tag == "lever")
        {
            leverController = null;
            onLever = false;
        }
        if (col.gameObject.tag == "ResetMachine")
        {
            resetMachine = false;
        }
        if (col.gameObject.tag == "LeverandShut")
        {
            leverAndShut = null;
            onLeverandShut = false;
        }
    }


}
