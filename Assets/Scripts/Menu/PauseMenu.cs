using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 0;
    }


    public void HandleResumeButtonOnClickEvent()
    {
        Time.timeScale = 1;
        MenuManager.paused = false;
        Destroy(gameObject);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Time.timeScale = 1;
        MenuManager.GoToMenu(MenuName.Main);
        MenuManager.paused = false;
        Destroy(gameObject);

    }

}
