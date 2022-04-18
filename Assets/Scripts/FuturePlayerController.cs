using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FuturePlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer futureSpriteRenderer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private LeverController leverController;
    private LeverandShut leverAndShutController;
    public LayerMask dramaLayer;

    const String playerRun = "playerRunning";
    const String playerIdle = "Idle";
    const String playerrunOnButton = "PlayerRunOnButton";
    const String playerIdleOnButton = "PlayerIdleButton";

    bool onLever = false;
    bool onLeverandShut = false;
    bool onTimeMachine = false;
    bool onResetMachine = false;
    bool playerDirectionRight = true;

    bool m_HitDetect;
    RaycastHit m_Hit;
    // Start is called before the first frame update
    void Start()
    {
        handleAnimation(playerIdle);
    }

    // Update is called once per frame
    void Update()
    {
        bool pastDrama()
        {
            if (playerDirectionRight)
            {
                RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size + new Vector3(0, .1f, 0), 0f, Vector2.right, 5f);
                if(raycastHit2d.transform.tag == "drama")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size + new Vector3(0, .1f, 0), 0f, Vector2.left, 5f);
                if (raycastHit2d.transform.tag == "drama")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        if (pastDrama())
        {
            Debug.Log("See the drama");
        }
        else
        {
            Debug.Log("Dont see any drama");
        }
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
