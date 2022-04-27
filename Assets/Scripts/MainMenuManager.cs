using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private Button openCredits;
    [SerializeField] private Button closeCredits;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject creditsUI;
    // Start is called before the first frame update
    void Awake()
    {

        openCredits.onClick.AddListener(() => creditsUI.SetActive(true));
        closeCredits.onClick.AddListener(() => creditsUI.SetActive(false));
        startGameButton.onClick.AddListener(() => LoadingScreen.LoadScene("Intro"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
