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

    [SerializeField]
    private Lamp[] lamps;
    [SerializeField]
    private SwitchButtonObjects wallToChickenMother;

    private int keyCount = 0;

    private void Start()
    {
        PublicGameManager = this;
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
