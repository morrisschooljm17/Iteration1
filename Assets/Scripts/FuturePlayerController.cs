using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuturePlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
    private LeverController leverController;
    private LeverandShut leverAndShutController;

    const String playerRun = "playerRunning";
    const String playerIdle = "Idle";
    const String playerrunOnButton = "PlayerRunOnButton";
    const String playerIdleOnButton = "PlayerIdleButton";

    bool onLever = false;
    bool onLeverandShut = false;
    bool onTimeMachine = false;
    bool onResetMachine = false;
    // Start is called before the first frame update
    void Start()
    {
        handleAnimation(playerIdle);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool moveFuturePlayer(Vector2 direction, Vector2 move,  bool hitTime, bool hitLever, bool hitLevernadShut, float time)
    {
        StartCoroutine(MoveFutureSelf());
        IEnumerator MoveFutureSelf()
        {
            yield return new WaitForSeconds(time);
            if (direction.x < 0)
            {
                futureSpriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                futureSpriteRenderer.flipX = false;
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

        private void handleAnimation(String anim){
        if(Equals(anim, playerRun)){
            if(onLever || onLeverandShut || onTimeMachine || onResetMachine){
                playerAnimator.Play(playerrunOnButton);
            }
            else{
                playerAnimator.Play(playerRun);
            }
        }
        else if(Equals(anim, playerIdle)){
            if(onLever || onLeverandShut || onTimeMachine || onResetMachine){
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
    }


}
