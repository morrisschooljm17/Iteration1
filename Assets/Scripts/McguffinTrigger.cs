using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class McguffinTrigger : MonoBehaviour
{
    private bool mcguffinTrigger;
    public PlayerController ps;
    public TMP_Text textObject;

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

            if (textObject != null) {

                Debug.Log("HERE");

                textObject.enabled = true;
            }

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
