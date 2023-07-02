using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Managing the base Menu things to go to main menu, exit game or start the level :)
 */
public class UIManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartAndRestart()
    {
        SceneManager.LoadScene("MainLevel");
    }

}
