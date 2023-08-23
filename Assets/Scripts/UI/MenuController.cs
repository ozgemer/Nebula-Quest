using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        GameObject score = GameObject.Find("Score");
        if (score)        
            score.GetComponent<TextMeshProUGUI>().text += score.GetComponent<GemsCounter>().GemsCount().ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
