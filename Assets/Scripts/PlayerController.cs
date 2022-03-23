using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mainRigidbody;
    [SerializeField] private Rigidbody2D futureBody;
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    bool isgrounded = true;
    bool flap = true;

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
        if (Input.GetKeyDown(KeyCode.W) && isgrounded)
        {
            isgrounded = false;
            move.y = jumpSpeed;
        }
        mainRigidbody.velocity = move;

        StartCoroutine(MoveFutureSelf(move));
        IEnumerator MoveFutureSelf(Vector3 move)
        {
            yield return new WaitForSeconds(5f);
            futureBody.velocity = move;
        }
        /*        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
                {
                    isgrounded = false;
                    mainRigidbody.AddForce(new Vector2(0, 500));

                }*/

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isgrounded = true;
            flap = true;
        }
    }

}
