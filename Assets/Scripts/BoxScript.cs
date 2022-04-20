using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "DramaBox" || col.gameObject.tag == "Player"){
            transform.tag = "DramaBox";
        }
    }
}
