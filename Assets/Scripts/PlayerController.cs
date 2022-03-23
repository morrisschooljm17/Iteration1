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
    [SerializeField] private float jumpSpeed;
    public LayerMask groundLayer;

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

/*    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isgrounded = true;
            flap = true;
        }
    }*/

}
