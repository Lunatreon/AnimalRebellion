using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSlaugtherhouse : MonoBehaviour
{
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
    public void playerGotCaught()
    {

    }
    public void playerGotChickenMother()
    {

    }
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
