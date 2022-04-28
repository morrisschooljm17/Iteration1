using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] GameObject whereShouldIBe;

    public void pickedUpByPastPlayer(){
        whereShouldIBe.GetComponent<BoxCollider2D>().enabled = false;
        whereShouldIBe.transform.parent = transform;
        whereShouldIBe.transform.position = transform.position;
    }
    public void pickedUpByCurrentPlayer(){
        whereShouldIBe.GetComponent<BoxCollider2D>().enabled = true;
        whereShouldIBe.transform.parent = null;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "DramaBox" || col.gameObject.tag == "Player" || col.gameObject.tag == "MovedBox"){
            if(transform.tag != "DramaBox"){
                transform.tag = "MovedBox";
            }
            pickedUpByCurrentPlayer();
            
        }
    }
}
