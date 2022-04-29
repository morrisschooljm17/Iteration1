using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheCure : MonoBehaviour
{
    [SerializeField] private GameObject CuredPlayer;
    [SerializeField] private GameObject ThePlayer;
    [SerializeField] private Transform cameraMain;
    [SerializeField] private Transform theText;
    private bool onCure;
    private GameObject newPlayer;
    bool cured = false;
    float speed = .7f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onCure)
        {
            transform.position = new Vector3(50,50,50);
            newPlayer = Instantiate(CuredPlayer);
            newPlayer.transform.position = ThePlayer.transform.position;
            Destroy(ThePlayer);
            cured = true;
            StartCoroutine(TheEnd());

        }
        if(cured){
            //cameraMain.position = Vector3.MoveTowards(cameraMain.position, newPlayer.transform.position, speed * Time.deltaTime);
            cameraMain.position = Vector3.MoveTowards(cameraMain.position, theText.transform.position, speed * Time.deltaTime);
        }

    }
    IEnumerator TheEnd(){        
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(0);

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        onCure = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        onCure = false;
    }

}
