using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McguffinTrigger : MonoBehaviour
{
    private bool mcguffinTrigger;
    public PlayerController ps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && mcguffinTrigger)
        {

            Destroy(gameObject);
            ps.canSnap = true;

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        mcguffinTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        mcguffinTrigger = false;
    }
}
