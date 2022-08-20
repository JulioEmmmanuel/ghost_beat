using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void HandleRetryButtonOnClickEvent()
    {
        SceneManager.LoadScene("Game");
    }

    public void HandleExitButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}
