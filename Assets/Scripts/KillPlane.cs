using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlane : MonoBehaviour
{

    void OnCollisionEnter2d(Collision otherObj)
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    }
}
