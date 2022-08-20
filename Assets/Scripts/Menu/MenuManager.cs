using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager 
{
    public static bool paused;
    

    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.Main:
                SceneManager.LoadScene("MainMenu");
                break;
            case MenuName.Pause:
                if (!paused)
                {
                    Object.Instantiate(Resources.Load("Pause menu"));
                    paused = true;
                }
            break;
            case MenuName.GameOver:
                SceneManager.LoadScene("GameOver");
                break;
        }
    }
}
