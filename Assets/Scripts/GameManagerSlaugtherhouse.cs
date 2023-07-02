using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Gamemanger which should manage all processing in the game, like collecting keys, switch the lamps or start the winning or death scene
 */
public class GameManagerSlaugtherhouse : UIManager
{
    //static because i don'T want to give EVERY object the game manager which must interact with it
    public static GameManagerSlaugtherhouse PublicGameManager { get; private set; }

    [Tooltip("Lamps for the cage of the chicken mother")]
    [SerializeField]
    private Lamp[] lamps;
    [Tooltip("Cage door which should be oppend if all keys are collected")]
    [SerializeField]
    private SwitchButtonObjects wallToChickenMother;

    [Tooltip("UI for the pause menu")]
    [SerializeField]
    private GameObject menu;
    private bool isPaused = false;



    private int keyCount = 0;

    private void Start()
    {
        Hock.changeMovement(true);
        Time.timeScale = 1;
        PublicGameManager = this;
        menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = !isPaused;
                Time.timeScale = 1;
                menu.SetActive(false);
            }
            else
            {
                isPaused = !isPaused;
                Time.timeScale = 0;
                menu.SetActive(true);
            }
        }
    }
    /*
     * player got caught by the guard or is hit by a mashine
     */
    public void playerGotCaught()
    {
        SceneManager.LoadScene("DeathScene");
    }
    /*
     * player got their chicken mother :)
     */
    public void playerGotChickenMother()
    {
        SceneManager.LoadScene("WinningScene");
    }

    /*
     * change lamp if player got a key and move wall down 
     */
    public void playerGotKey()
    {
        if(keyCount < lamps.Length)
        {
            lamps[keyCount].changeColor();
            keyCount++;
            if(keyCount == lamps.Length)
            {
                wallToChickenMother.TriggerChanged(true);
            }
        }
    }
}
