using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

    private bool keyTrigger;
    public NextLevel NL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && keyTrigger)
        {
            Destroy(gameObject);
            NL.keyExists = false;

        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        keyTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        keyTrigger = false;
    }
}
