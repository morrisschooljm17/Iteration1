using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite greenBox;
    [SerializeField] Sprite redBox;
    [SerializeField] Transform pressurePlate;
    [SerializeField] SmoothDoor[] doorsToOpen;
    private Vector3 originalPosition;
    private Vector3 newRedPlatePos;
    private bool playerOn = false;
    private bool doorsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = pressurePlate.position;
        newRedPlatePos = pressurePlate.position + new Vector3(0, -.15f, 0);
    }

    private void OpenOrCloseAllTheDoors(){
        foreach(SmoothDoor door in doorsToOpen){
            door.openDoor();
        }
    }
    private LinkedList<int> boxes = new LinkedList<int>();
    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){         
            playerOn = true;
        }
        if(col.gameObject.tag == "MovingBox" || col.gameObject.tag == "DramaBox" || col.gameObject.tag == "MovedBox"){
            boxes.AddLast(1);
        }
        spriteRenderer.sprite = greenBox;
        pressurePlate.transform.position = newRedPlatePos;
        if(doorsOpen == false){
            OpenOrCloseAllTheDoors();
            doorsOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            playerOn = false;
        }
        if(col.gameObject.tag == "MovingBox" || col.gameObject.tag == "DramaBox" || col.gameObject.tag == "MovedBox"){
            boxes.RemoveFirst();
        }
        if(playerOn == false && boxes.Count == 0){
                pressurePlate.position = originalPosition;
                spriteRenderer.sprite = redBox;
                OpenOrCloseAllTheDoors();
                doorsOpen = false;
            }

    }
}
