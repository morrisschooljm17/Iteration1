using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStopper : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
            rigidBody.isKinematic = true;
    }
}
