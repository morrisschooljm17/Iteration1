using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class FuturePlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private Rigidbody2D thePlayer;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private bool futureDrama;
    [SerializeField] private GameObject[] avoidTheseThings;
    [SerializeField] private Transform[] futureBoxPositions;

    
    private LeverController leverController;
    private LeverandShut leverAndShutController;
    private SmoothDoorController elevator;
    public LayerMask dramaLayer;
    private Rigidbody2D movingBox;
    private Rigidbody2D boxBeingHeld;

    const String playerRun = "playerRunning";
    const String playerIdle = "Idle";
    const String playerrunOnButton = "PlayerRunOnButton";
    const String playerIdleOnButton = "PlayerIdleButton";

    bool onLever = false;
    bool onLeverandShut = false;
    bool onTimeMachine = false;
    bool onResetMachine = false;
    bool playerDirectionRight = true;
    bool onElevator = false;
    bool holdingBox = false;
    bool onMovingBox;
    bool m_HitDetect;
    RaycastHit m_Hit;
    Vector3 rayCastStartRight;
    Vector3 rayCastStartLeft;

    // Start is called before the first frame update
    void Start()
    {
        handleAnimation(playerIdle);
    }

    // Update is called once per frame
    void Update()
    {
        if(futureDrama){
            rayCastStartRight = boxCollider2D.bounds.center + new Vector3(.55f,.77f,0);
            rayCastStartLeft = boxCollider2D.bounds.center + new Vector3(-.55f,.77f,0);
            bool pastDrama(){
                if(playerDirectionRight){
                    foreach(GameObject avoid in avoidTheseThings){
                        RaycastHit2D hit = Physics2D.Raycast(rayCastStartRight, avoid.transform.position-rayCastStartRight, 50f, dramaLayer);
                        //Debug.DrawRay(rayCastStartRight, avoid.transform.position-rayCastStartRight, Color.green);
                        if(hit.collider != null && (hit.transform.tag == "drama" || hit.transform.tag == "Player" || hit.transform.tag == "DramaBox")){
                            return true;
                        }
                    }
                }
                else{
                    foreach(GameObject avoid in avoidTheseThings){
                        RaycastHit2D hit = Physics2D.Raycast(rayCastStartLeft, avoid.transform.position-rayCastStartLeft, 50f, dramaLayer);
                        //Debug.DrawRay(rayCastStartLeft, avoid.transform.position-rayCastStartLeft, Color.red);
                        if(hit.collider != null && (hit.transform.tag == "drama" || hit.transform.tag == "Player" || hit.transform.tag == "DramaBox")){
                            return true;
                        }
                    }  
                }
                return false;
            }
            if (pastDrama())
            {
                futureDrama = false;
                StartCoroutine(SpinActualPlayerToDEATH(thePlayer));
            }
        }
    }
    public bool moveFuturePlayer(Vector2 direction, Vector2 move,  bool hitTime, bool hitLever, bool hitLevernadShut, 
    bool elevator, bool grabbedBox, bool droppedBox, Vector3[] boxPos, bool pastHoldingBox, float time)
    {
        StartCoroutine(MoveFutureSelf());
        IEnumerator MoveFutureSelf()
        {
            yield return new WaitForSeconds(time);
            if (direction.x < 0)
            {
                futureSpriteRenderer.flipX = true;
                if(holdingBox){
                    boxBeingHeld.transform.position = transform.position + new Vector3(-1f, -.05f, 0);
                }
                playerDirectionRight = false;
            }
            else if (direction.x > 0)
            {
                futureSpriteRenderer.flipX = false;
                if(holdingBox){
                    boxBeingHeld.transform.position = transform.position + new Vector3(1f, -.05f, 0);
                }
                playerDirectionRight = true;
            }
            if (grabbedBox || pastHoldingBox){
                if (onMovingBox && (holdingBox == false)){
                    boxBeingHeld = movingBox;
                    boxBeingHeld.transform.parent = transform;
                    boxBeingHeld.simulated = false;
                    boxBeingHeld.GetComponent<BoxScript>().pickedUpByPastPlayer();
                    grabbedBox = true;
                    holdingBox = true;
                    if(playerDirectionRight){boxBeingHeld.transform.position = transform.position + new Vector3(1f, -.05f, 0);}
                    else{boxBeingHeld.transform.position = transform.position + new Vector3(-1f, -.05f, 0);}
                }
            }
            else if(droppedBox){
                boxBeingHeld.transform.parent = null;
                boxBeingHeld.simulated = true;
                boxBeingHeld = null;
                holdingBox = false;
            }
            if (hitTime)
            {
                StartCoroutine(SpinPlayer(futureBody));               
            }
            else if (hitLever)
            {
                leverController.openDoor();
            }
            else if (hitLevernadShut)
            {
                leverAndShutController.activate();
            }
            else if (elevator)
            {
                this.elevator.startElevator();
            }
            if(Math.Abs(direction.x) >= .3){
                handleAnimation(playerRun);
            }
            else{
                handleAnimation(playerIdle);
            }

            futureBody.position = move + new Vector2(50, 0);
            for(int i = 0; i < boxPos.Length; i++){
                if((holdingBox && !futureBoxPositions[i].GetComponent<Rigidbody2D>().simulated) || futureBoxPositions[i].tag.Equals("DramaBox") || futureBoxPositions[i].tag.Equals("MovedBox")){}
                else{
                    futureBoxPositions[i].position = boxPos[i] + new Vector3(50, 0, 0);
                }              
            }
            

        }
        return !hitTime;
    }
        IEnumerator SpinActualPlayerToDEATH(Rigidbody2D player)
    {
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
        Destroy(transform.gameObject);
    }

        private void handleAnimation(String anim){
        if(Equals(anim, playerRun)){
            if(onLever || onLeverandShut || onTimeMachine || onResetMachine || onElevator || onMovingBox || holdingBox){
                playerAnimator.Play(playerrunOnButton);
            }
            else{
                playerAnimator.Play(playerRun);
            }
        }
        else if(Equals(anim, playerIdle)){
            if(onLever || onLeverandShut || onTimeMachine || onResetMachine || onElevator || onMovingBox || holdingBox){
                playerAnimator.Play(playerIdleOnButton);
            }
            else{
                playerAnimator.Play(playerIdle);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "drama" || col.gameObject.tag == "DramaBox")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void OnTriggerStay2D(Collider2D col){
                if(col.gameObject.tag == "MovingBox" || col.gameObject.tag == "MovedBox"){
            onMovingBox = true;
            movingBox = col.gameObject.GetComponent<Rigidbody2D>();
        }
    }
        private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "TimeMachine")
        {
            onTimeMachine = true;
        }
        if (col.gameObject.tag == "lever")
        {
            leverController = col.GetComponent<LeverController>();
            onLever = true;
        }
        if (col.gameObject.tag == "ResetMachine")
        {
            onResetMachine = true;
        }
        if (col.gameObject.tag == "LeverandShut")
        {
            leverAndShutController = col.GetComponent<LeverandShut>();
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

            onTimeMachine = false;
        }
        if (col.gameObject.tag == "lever")
        {
            leverController = null;
            onLever = false;
        }
        if (col.gameObject.tag == "ResetMachine")
        {
            onResetMachine = false;
        }
        if (col.gameObject.tag == "LeverandShut")
        {
            leverAndShutController = null;
            onLeverandShut = false;
        }
        if (col.gameObject.tag == "SmoothDoor")
        {
            elevator = null;
            onElevator = false;
        }
        if(col.gameObject.tag.Equals("MovingBox") || col.gameObject.tag.Equals("MovedBox")){
            onMovingBox = false;
            movingBox = null;
        }

    }

public bool moveFuturePlayer(Vector2 direction, Vector2 move,  bool hitTime, bool hitLever, bool hitLevernadShut, 
    bool elevator, float time)
    {
        StartCoroutine(MoveFutureSelf());
        IEnumerator MoveFutureSelf()
        {
            yield return new WaitForSeconds(time);
            if (direction.x < 0)
            {
                futureSpriteRenderer.flipX = true;
                playerDirectionRight = false;
            }
            else if (direction.x > 0)
            {
                futureSpriteRenderer.flipX = false;
                playerDirectionRight = true;
            }

            if (hitTime)
            {
                StartCoroutine(SpinPlayer(futureBody));
                
            }
            else if (hitLever)
            {
                leverController.openDoor();
            }
            else if (hitLevernadShut)
            {
                leverAndShutController.activate();
            }
            else if (elevator)
            {
                this.elevator.startElevator();
            }
            if(Math.Abs(direction.x) >= .3){
                handleAnimation(playerRun);
            }
            else{
                handleAnimation(playerIdle);
            }

            futureBody.position = move + new Vector2(50, 0);
            

        }
        return !hitTime;
    }

}
