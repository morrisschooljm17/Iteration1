using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Camera cameraMove;
    [SerializeField] private FuturePlayerController futurePlayerController;
    [SerializeField] private float futurePlayerDelay;
    [SerializeField] private Animator playerAnimator;
    private TimeMachine timeMachine;
    private LeverController leverController;
    private LeverandShut leverAndShut;
    private SmoothDoorController elevator;
    public LayerMask groundLayer;
    private string sceneName;
    private bool inPresent;

    bool onTimeMachine = false;
    bool onLever = false;
    bool resetMachine = false;
    bool onLeverandShut = false;
    bool onElevator = false;
    public bool isThereAFuturePlayer;

    const String playerRun = "playerRunning";
    const String playerIdle = "Idle";
    const String playerRunOnButton = "PlayerRunOnButton";
    const String playerIdleOnButton = "PlayerIdleButton";


    // Start is called before the first frame update
    void Start()
    {
        inPresent = true;
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;

    }

    // Update is called once per frame
    void Update()
    {

        bool hitTime = false;
        bool hitLever = false;
        bool hitLeverandShut = false;
        bool hitElevator = false;

        bool IsGrounded()
        {

            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size + new Vector3(0, .1f, 0), 0f, Vector2.down, .02f, groundLayer);
            return raycastHit2d.collider != null;
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {

            if (sceneName == "level4" && inPresent) {                
                transform.position += new Vector3(50, 0, 0);
                cameraMove.transform.position += new Vector3(50, 0, 0);
                inPresent = false;                               
            }

            else if (sceneName == "level4" && !inPresent)
            {               
                transform.position += new Vector3(-50, 0, 0);
                cameraMove.transform.position += new Vector3(-50, 0, 0);
                inPresent = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onTimeMachine)
            {
                timeMachine.timeTravel(mainRigidbody, cameraMove);
                hitTime = true;
            }
            else if (onLever)
            {
                leverController.openDoor();
                hitLever = true;
            }
            else if (resetMachine)
            {
                StartCoroutine(SpinPlayer(mainRigidbody));

            }
            else if (onLeverandShut)
            {
                leverAndShut.activate();
                hitLeverandShut = true;
            }
            else if (onElevator)
            {
                elevator.startElevator();
                hitElevator = true;
            }
        }

        Vector2 move = mainRigidbody.velocity;
        float hor = Input.GetAxis("Horizontal");
        if (hor < 0)
        {
            mainSpriteRenderer.flipX = true;
        }
        else if (hor > 0)
        {
            mainSpriteRenderer.flipX = false;
        }

        move.x = hor * moveSpeed;
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            move.y = jumpSpeed;

        }
        if(Math.Abs(move.x) >= .3){
            handleAnimation(playerRun);
        }
        else{
            handleAnimation(playerIdle);
        }
        mainRigidbody.velocity = move;
        
        if (isThereAFuturePlayer)
        {
            isThereAFuturePlayer = futurePlayerController.moveFuturePlayer(move, transform.position, hitTime, hitLever, hitLeverandShut, hitElevator, futurePlayerDelay);
        }
    }

    IEnumerator SpinPlayer(Rigidbody2D player)
    {
        Vector3 local = player.transform.localScale;
        Vector3 position = player.transform.position;

        for (int i = 0; i < 50; i++)
        {

                player.transform.localScale += new Vector3(-.1f, -.1f, 0);
                player.transform.position = position;


            player.transform.Rotate(Vector3.forward * -45);
            yield return new WaitForSeconds(.01f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void handleAnimation(String anim){
        if(Equals(anim, playerRun)){
            if(onLever || onLeverandShut || onTimeMachine){
                playerAnimator.Play(playerRunOnButton);
            }
            else{
                playerAnimator.Play(playerRun);
            }
        }
        else if(Equals(anim, playerIdle)){
            if(onLever || onLeverandShut || onTimeMachine){
                playerAnimator.Play(playerIdleOnButton);
            }
            else{
                playerAnimator.Play(playerIdle);
            }
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
        if (col.gameObject.tag == "SmoothDoor")
        {
            elevator = col.GetComponent<SmoothDoorController>();
            onElevator = true;
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
        if (col.gameObject.tag == "SmoothDoor")
        {
            elevator = null;
            onElevator = false;
        }
    }


}
