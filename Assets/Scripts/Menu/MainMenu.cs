
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void HandlePlayButtonOnClickEvent()
    {
       SceneManager.LoadScene("Game");
    }

    public void HandleExitButtonOnClickEvent()
    {
        Application.Quit();
    }

}
