using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : UIComponent
{
    public UnityAction startGame, quitGame;
    public void StartGame()
    {
        startGame();
    }

    public void Quit()
    {
        quitGame();
    }
}
