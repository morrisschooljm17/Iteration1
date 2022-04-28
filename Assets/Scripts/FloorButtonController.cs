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
    //private bool playerOn = false;
    //private bool doorsOpen = false;
    private int count = 0;
    private LinkedList<GameObject> boxes = new LinkedList<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = pressurePlate.position;
        newRedPlatePos = pressurePlate.position + new Vector3(0, -.15f, 0);
    }
    void Update(){
        if(count == 0){
            pressurePlate.position = originalPosition;
            spriteRenderer.sprite = redBox;
            CloseAllTheDoors();
        }
        else{
            spriteRenderer.sprite = greenBox;
            pressurePlate.transform.position = newRedPlatePos;
            OpenAllTheDoors();      
        }
    }

    private void OpenAllTheDoors(){
        foreach(SmoothDoor door in doorsToOpen){
            door.openDoor();
        }
    }
        private void CloseAllTheDoors(){
        foreach(SmoothDoor door in doorsToOpen){
            door.closeDoor();
        }
    }

    // private void OnTriggerEnter2D(Collider2D col){
    //     if(col.gameObject.tag == "Player"){         
    //         playerOn = true;
    //     }
    //     if(col.gameObject.tag == "MovingBox" || col.gameObject.tag == "DramaBox" || col.gameObject.tag == "MovedBox"){
    //         boxes.AddLast(col.gameObject);
    //     }
    //     spriteRenderer.sprite = greenBox;
    //     pressurePlate.transform.position = newRedPlatePos;
    //     if(doorsOpen == false){
    //         OpenOrCloseAllTheDoors();
    //         doorsOpen = true;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.tag == "MovingBox" || col.gameObject.tag == "DramaBox" || 
        col.gameObject.tag == "MovedBox" || col.gameObject.tag == "Player" || col.gameObject.tag == "FuturePlayer"){
            count++;
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "MovingBox" || col.gameObject.tag == "DramaBox" || 
        col.gameObject.tag == "MovedBox" || col.gameObject.tag == "Player" || col.gameObject.tag == "FuturePlayer"){
            count--;
        }


    }
}
